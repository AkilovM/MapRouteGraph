using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapRouteGraph
{
    public partial class Form1 : Form
    {
        bool pickingColor = false;
        bool markingRoom = false;

        Image image;
        List<Square> squares; // all squares
        Cell[,] arr;
        List<Square> squaresRooms = new List<Square> { }; // squares marked as rooms
        List<Square> squaresPaths = new List<Square> { }; // squares marked as paths
        List<Room> rooms = new List<Room> { };
        List<Point> points = new List<Point> { };
        List<Path> paths;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "C:";
            openFileDialog1.Title = "Open image";
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "PNG Images|*.png|JPEG Images|*.jpg|BITMAPS|*.bmp";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                image = Image.FromFile(openFileDialog1.FileName);
                pictureBox1.Image = image;
                pictureBox1.Width = pictureBox1.Image.Width;
                pictureBox1.Height = pictureBox1.Image.Height;
                buttonPickColor.Enabled = true;
                buttonGenerateRooms.Enabled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            pickingColor = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (StreamWriter file = File.CreateText(@"c:\graph.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, paths);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if(pickingColor)
            {
                pickingColor = false;
                using (var bmp = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height))
                {
                    pictureBox1.DrawToBitmap(bmp, pictureBox1.ClientRectangle);
                    var color = bmp.GetPixel(pictureBox1.PointToClient(MousePosition).X, pictureBox1.PointToClient(MousePosition).Y);
                    pictureWallColor.BackColor = color;
                }
            }
            else if(markingRoom)
            {
                var point = pictureBox1.PointToClient(MousePosition);
                Square square = null;
                foreach (var s in squares)
                    if (point.X >= s.x && point.X < s.x + s.width && point.Y >= s.y && point.Y < s.y + s.height)
                    {
                        square = s;
                        break;
                    }
                if (square != null)
                {
                    if (textBoxRoomName.TextLength > 0)
                    {
                        squaresRooms.Add(square);
                    }
                    else
                    {
                        squaresPaths.Add(square);
                    }
                    using (var bmp = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height))
                    {
                        pictureBox1.DrawToBitmap(bmp, pictureBox1.ClientRectangle);
                        for (var y = square.y; y < square.y + square.height; y++)
                            for (var x = square.x; x < square.x + square.width; x++)
                                if (x == square.x || x == square.x + square.width - 1 || y == square.y || y == square.y + square.height - 1)
                                    bmp.SetPixel(x, y, Color.OrangeRed);

                        pictureBox1.Image = Image.FromHbitmap(bmp.GetHbitmap());
                    }
                    buttonGenerateGraph.Enabled = true;
                }
            }
        }

        private void buttonGenerateGraph_Click(object sender, EventArgs e)
        {
            using (var bmp = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height))
            {
                pictureBox1.DrawToBitmap(bmp, pictureBox1.ClientRectangle);
                Color pixelColor;
                arr = new Cell[pictureBox1.Image.Width, pictureBox1.Image.Height];
                squares = new List<Square> { };

                int xLimit;
                int yLimit;

                int leftId;
                int upId;
                int rightId;
                int downId;
                Value leftValue;
                Value upValue;
                Value rightValue;
                Value downValue;

                // set all walls
                int walls = 0;
                for (var y = 0; y < pictureBox1.Image.Height; y++)
                    for (var x = 0; x < pictureBox1.Image.Width; x++)
                    {
                        arr[x, y] = new Cell();
                        pixelColor = bmp.GetPixel(x, y);
                        if (pixelColor == pictureWallColor.BackColor)
                        {
                            arr[x, y].value = Value.wall;
                            walls++;
                        }
                    }

                // set all squares
                for (var y = 0; y < pictureBox1.Image.Height; y++)
                    for (var x = 0; x < pictureBox1.Image.Width; x++)
                        if (arr[x, y].value == Value.freeSpace && arr[x, y].squareId == -1)
                        {
                            //set default 
                            xLimit = pictureBox1.Image.Width - 1;
                            yLimit = pictureBox1.Image.Height - 1;
                            leftId = -1;
                            upId = -1;
                            rightId = -1;
                            downId = -1;
                            leftValue = Value.freeSpace;
                            upValue = Value.freeSpace;
                            rightValue = Value.freeSpace;
                            downValue = Value.freeSpace;

                            //check neighbours of first pixel
                            //left
                            if (x > 0)
                            {
                                leftValue = arr[x - 1, y].value;
                                leftId = arr[x - 1, y].squareId;
                            }
                            //up
                            if (y > 0)
                            {
                                upValue = arr[x, y - 1].value;
                                upId = arr[x, y - 1].squareId;
                            }
                            //right
                            if (x < pictureBox1.Image.Width - 1)
                            {
                                rightValue = arr[x + 1, y].value;
                                rightId = arr[x + 1, y].squareId;
                            }
                            //down
                            if (y < pictureBox1.Image.Height - 1)
                            {
                                downValue = arr[x, y + 1].value;
                                downId = arr[x, y + 1].squareId;
                            }

                            //get new square
                            for (var y2 = y; y2 <= yLimit; y2++)
                                for (var x2 = x; x2 <= xLimit; x2++)
                                {
                                    //left
                                    if (x2 == x && x2 != 0)
                                        if (arr[x2 - 1, y2].value != leftValue || arr[x2 - 1, y2].squareId != leftId)
                                            yLimit = y2 - 1;
                                    //up
                                    if (y2 == y && y2 != 0)
                                        if (arr[x2, y2 - 1].value != upValue || arr[x2, y2 - 1].squareId != upId)
                                            xLimit = x2 - 1;

                                    //right
                                    if(x2 != pictureBox1.Image.Width - 1)
                                    {
                                        if (arr[x2 + 1, y2].value != Value.freeSpace || arr[x2 + 1, y2].squareId != -1)
                                            xLimit = x2;
                                        if (x2 == xLimit)
                                            if (y2 == y)
                                            {
                                                rightValue = arr[x2 + 1, y2].value;
                                                rightId = arr[x2 + 1, y2].squareId;
                                            }
                                            else if (arr[x2 + 1, y2].value != rightValue || arr[x2 + 1, y2].squareId != rightId)
                                                yLimit = y2 - 1;
                                    }

                                    //down
                                    if(y2 != pictureBox1.Image.Height - 1)
                                    {
                                        if (arr[x2, y2 + 1].value != Value.freeSpace || arr[x2, y2 + 1].squareId != -1)
                                            yLimit = y2;
                                        if (y2 == yLimit)
                                            if (x2 == x)
                                            {
                                                downValue = arr[x2, y2 + 1].value;
                                                downId = arr[x2, y2 + 1].squareId;
                                            }
                                            else if (arr[x2, y2 + 1].value != downValue || arr[x2, y2 + 1].squareId != downId)
                                                xLimit = x2 - 1;
                                    }
                                }

                            int width = xLimit - x + 1;
                            int height = yLimit - y + 1;
                            if (Square.CheckSize(width, height))
                            {
                                Square square = new Square(x, y, width, height, squares.Count);
                                squares.Add(square);
                                for (var y2 = y; y2 <= yLimit; y2++)
                                    for (var x2 = x; x2 <= xLimit; x2++)
                                    {
                                        arr[x2, y2].value = Value.exploredSpace;
                                        arr[x2, y2].squareId = square.id;
                                    }
                            }
                            else 
                                for (var y2 = y; y2 <= yLimit; y2++)
                                    for (var x2 = x; x2 <= xLimit; x2++)
                                        arr[x2, y2].value = Value.smallSpace;
                        }
                foreach (var s in squares)
                {
                    for (var y = s.y; y < s.y + s.height; y++)
                        for (var x = s.x; x < s.x + s.width; x++)
                            if (x == s.x || x == s.x + s.width - 1 || y == s.y || y == s.y + s.height - 1)
                                bmp.SetPixel(x, y, Color.Green);
                }
                pictureBox1.Image = Image.FromHbitmap(bmp.GetHbitmap());
            }
            buttonMarkRoom.Enabled = true;
        }

        private void buttonMarkRoom_Click(object sender, EventArgs e)
        {
            markingRoom = !markingRoom;
        }

        private void buttonGenerateGraph_Click_1(object sender, EventArgs e)
        {
            markingRoom = false;
            paths = new List<Path> { };
            var roomsId = new List<int> { };
            foreach (var r in squaresRooms)
                roomsId.Add(r.id);
            //BFS
            var firstRoom = squaresRooms[0];
            roomsId.Remove(squaresRooms[0].id);
            Queue<Square> queue = new Queue<Square> { };
            queue.Enqueue(firstRoom);
            List<int> used = new List<int> { firstRoom.id };
            var p = new Dictionary<int, int> { };
            while (queue.Count > 0 && roomsId.Count > 0)
            {
                var s = queue.Dequeue();
                for(var x = s.x; x < s.x + s.width; x++)
                {
                    if (s.y != 0)
                        if (arr[x, s.y - 1].squareId != -1 && !used.Contains(arr[x, s.y - 1].squareId))
                        {
                            used.Add(arr[x, s.y - 1].squareId);
                            queue.Enqueue(squares[arr[x, s.y - 1].squareId]);
                            p.Add(arr[x, s.y - 1].squareId, s.id);
                            if (roomsId.Contains(arr[x, s.y - 1].squareId))
                                roomsId.Remove(arr[x, s.y - 1].squareId);
                        }
                    if (s.y + s.height - 1 != pictureBox1.Image.Height)
                        if (arr[x, s.y + s.height].squareId != -1 && !used.Contains(arr[x, s.y + s.height].squareId))
                        {
                            used.Add(arr[x, s.y + s.height].squareId);
                            queue.Enqueue(squares[arr[x, s.y + s.height].squareId]);
                            p.Add(arr[x, s.y + s.height].squareId, s.id);
                            if (roomsId.Contains(arr[x, s.y + s.height].squareId))
                                roomsId.Remove(arr[x, s.y + s.height].squareId);
                        }
                }
                for (var y = s.y; y < s.y + s.height; y++)
                {
                    if(s.x != 0)
                        if (arr[s.x - 1, y].squareId != -1 && !used.Contains(arr[s.x - 1, y].squareId))
                        {
                            used.Add(arr[s.x - 1, y].squareId);
                            queue.Enqueue(squares[arr[s.x - 1, y].squareId]);
                            p.Add(arr[s.x - 1, y].squareId, s.id);
                            if (roomsId.Contains(arr[s.x - 1, y].squareId))
                                roomsId.Remove(arr[s.x - 1, y].squareId);
                        }
                    if (s.x + s.width - 1 != pictureBox1.Image.Width)
                        if(arr[s.x + s.width, y].squareId != -1 && !used.Contains(arr[s.x + s.width, y].squareId))
                        {
                            used.Add(arr[s.x + s.width, y].squareId);
                            queue.Enqueue(squares[arr[s.x + s.width, y].squareId]);
                            p.Add(arr[s.x + s.width, y].squareId, s.id);
                            if (roomsId.Contains(arr[s.x + s.width, y].squareId))
                                roomsId.Remove(arr[s.x + s.width, y].squareId);
                        }
                }
            }
            queue.Clear();
            foreach (var r in squaresRooms)
                queue.Enqueue(r);
            used.Clear();
            while(queue.Count > 0)
            {
                var r = queue.Dequeue();
                if (!used.Contains(r.id))
                {
                    used.Add(r.id);
                    if(p.ContainsKey(r.id))
                    {
                        queue.Enqueue(squares[p[r.id]]);
                        paths.Add(new Path(r.x + r.width / 2, r.y + r.height / 2, squares[p[r.id]].x + squares[p[r.id]].width / 2, squares[p[r.id]].y + squares[p[r.id]].height / 2));
                    }
                }
            }
            using (var bmp = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height))
            {
                pictureBox1.Image = image;
                pictureBox1.DrawToBitmap(bmp, pictureBox1.ClientRectangle);
                foreach(var path in paths)
                {
                    BresenhamLine(path.point1.x, path.point1.y, path.point2.x, path.point2.y, bmp);
                }
                pictureBox1.Image = Image.FromHbitmap(bmp.GetHbitmap());
            }
            buttonExportJSON.Enabled = true;
        }

        void BresenhamLine(int x0, int y0, int x1, int y1, Bitmap bmp)
        {
            var steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
            if (steep)
            {
                Swap(ref x0, ref y0);
                Swap(ref x1, ref y1);
            }
            if (x0 > x1)
            {
                Swap(ref x0, ref x1);
                Swap(ref y0, ref y1);
            }
            int dx = x1 - x0;
            int dy = Math.Abs(y1 - y0);
            int error = dx / 2;
            int ystep = (y0 < y1) ? 1 : -1;
            int y = y0;
            for (int x = x0; x <= x1; x++)
            {
                for (var xr = -1; xr < 2; xr++)
                    for (var yr = -1; yr < 2; yr++)
                        if ((steep ? y : x) + xr >= 0 && (steep ? y : x) + xr < bmp.Width && (steep ? x : y) + yr >=0 && (steep ? x : y) + yr < bmp.Height)
                            bmp.SetPixel(steep ? y + yr : x + xr, steep ? x + xr : y + yr, Color.Red);
                error -= dy;
                if (error < 0)
                {
                    y += ystep;
                    error += dx;
                }
            }
        }

        void Swap(ref int a, ref int b)
        {
            int c = a;
            a = b;
            b = c;
        }
    }

    public enum Value
    {
        freeSpace,
        exploredSpace,
        smallSpace,
        wall
    }

    public class Cell
    {
        public Value value;
        public int squareId;

        public Cell()
        {
            value = Value.freeSpace;
            squareId = -1;
        }
    }

    public class Square
    {
        public int width;
        public int height;
        public int x;
        public int y;
        static int minWidth = 2;
        static int minHeight = 2;
        public int id;

        public Square(int x, int y, int width, int height, int id)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.id = id;
        }

        public static bool CheckSize(int width, int height)
        {
            return width >= minWidth && height >= minHeight;
        }
    }

    public class Point
    {
        public int x;
        public int y;

        public Point()
        {
            x = 0;
            y = 0;
        }

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class Room : Point
    {
        public string name;

        public Room()
        {
            x = 0;
            y = 0;
            name = string.Empty;
        }

        public Room(int x, int y, string name)
        {
            this.x = x;
            this.y = y;
            this.name = name;
        }
    }

    public class Path
    {
        public Point point1;
        public Point point2;

        public Path(int x1, int y1, int x2, int y2)
        {
            point1 = new Point(x1, y1);
            point2 = new Point(x2, y2);
        }
    }
}
