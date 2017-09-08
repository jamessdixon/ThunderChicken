using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.XPath;

namespace CommonComponent
{
    public class UI
    {
        private const string ButtonType = "button";

        private const string CheckType = "check";

        private const string StaticType = "static";

        private const string RadioType = "radio";

        private const string TrackbarType = "trackbar";

        private const string CustomType = "custom";

        private const string Control = "control";

        private Form myForm;

        public PictureBox myPictureBox;

        public ToolTip myToolTip;

        private string uiPath;

        private string xmlPath;

        private Bitmap picForeground;

        private Bitmap picHighlight;

        private Bitmap picBackground;

        private Point cursorPos;

        private bool isFormMoving;

        private int ToolTipCnt;

        public List<UI.Element> elementlist;

        public UI(Form aForm, PictureBox aPictureBox, ToolTip aToolTip, string path, string xmlFileName)
        {
            this.cursorPos = new Point();
            this.myForm = aForm;
            this.myPictureBox = aPictureBox;
            this.myToolTip = aToolTip;
            this.uiPath = path;
            this.xmlPath = Path.Combine(this.uiPath, xmlFileName);
            this.elementlist = new List<UI.Element>();
            this.Init_FormUI();
        }

        public void AddElement(string name, string type, Point aPoint, Size aSize, string tooltip)
        {
            UI.Element element = new UI.Element()
            {
                Name = name,
                Type = type,
                Tooltip = tooltip
            };
            element.Rect.Location = aPoint;
            element.Rect.Size = aSize;
            element.Rect2 = element.Rect;
            element.ImageHighlight = this.picHighlight.Clone(element.Rect, this.picHighlight.PixelFormat);
            element.ImageForeground = this.picForeground.Clone(element.Rect, this.picForeground.PixelFormat);
            if (tooltip != null)
            {
                element.hasToolTip = true;
            }
            else
            {
                element.hasToolTip = false;
            }
            if (type == "static")
            {
                element.isDisplayFore = true;
            }
            element.Exec = null;
            this.elementlist.Add(element);
        }

        public void AddEvent(string name, UI.FunctionPointer fn)
        {
            foreach (UI.Element element in this.elementlist)
            {
                if (element.Name != name)
                {
                    continue;
                }
                element.Exec = fn;
            }
        }

        public UI.Element GetElementByName(string name)
        {
            UI.Element element = null;
            foreach (UI.Element element1 in this.elementlist)
            {
                if (element1.Name != name)
                {
                    continue;
                }
                element = element1;
            }
            return element;
        }

        private string GetInnerContent(XPathNodeIterator parent, string xpath)
        {
            XPathNodeIterator xPathNodeIterators = parent.Current.Select(xpath);
            if (!xPathNodeIterators.MoveNext())
            {
                return null;
            }
            return xPathNodeIterators.Current.InnerXml;
        }

        public void Init_FormUI()
        {
            string innerContent;
            string str;
            try
            {
                XPathNavigator xPathNavigator = (new XPathDocument(this.xmlPath)).CreateNavigator();
                xPathNavigator.MoveToRoot();
                XPathNodeIterator xPathNodeIterators = xPathNavigator.Select("/form/property");
                while (xPathNodeIterators.MoveNext())
                {
                    str = this.GetInnerContent(xPathNodeIterators, "name");
                    string str1 = str;
                    string str2 = str1;
                    if (str1 == null)
                    {
                        continue;
                    }
                    if (str2 == "picBackground")
                    {
                        innerContent = this.GetInnerContent(xPathNodeIterators, "value");
                        innerContent = Path.Combine(this.uiPath, innerContent);
                        this.picBackground = new Bitmap(innerContent);
                        this.myPictureBox.Image = this.picBackground;
                        this.myForm.Size = this.picBackground.Size;
                        this.myPictureBox.Size = this.picBackground.Size;
                    }
                    else if (str2 == "picForeground")
                    {
                        innerContent = this.GetInnerContent(xPathNodeIterators, "value");
                        innerContent = Path.Combine(this.uiPath, innerContent);
                        this.picForeground = new Bitmap(innerContent);
                    }
                    else if (str2 == "picHighlight")
                    {
                        innerContent = this.GetInnerContent(xPathNodeIterators, "value");
                        innerContent = Path.Combine(this.uiPath, innerContent);
                        this.picHighlight = new Bitmap(innerContent);
                    }
                }
                xPathNavigator.MoveToRoot();
                xPathNodeIterators = xPathNavigator.Select("/form/region");
                Size num = new Size();
                Point point = new Point();
                while (xPathNodeIterators.MoveNext())
                {
                    str = this.GetInnerContent(xPathNodeIterators, "name");
                    string innerContent1 = this.GetInnerContent(xPathNodeIterators, "type");
                    point.X = Convert.ToInt32(this.GetInnerContent(xPathNodeIterators, "X"));
                    point.Y = Convert.ToInt32(this.GetInnerContent(xPathNodeIterators, "Y"));
                    num.Width = Convert.ToInt32(this.GetInnerContent(xPathNodeIterators, "Width"));
                    num.Height = Convert.ToInt32(this.GetInnerContent(xPathNodeIterators, "Height"));
                    string innerContent2 = this.GetInnerContent(xPathNodeIterators, "tooltip");
                    if (innerContent1 == "control")
                    {
                        Control item = this.myForm.Controls[str];
                        item.Size = num;
                        item.Location = point;
                    }
                    else
                    {
                        this.AddElement(str, innerContent1, point, num, innerContent2);
                        if (innerContent1 != "trackbar")
                        {
                            innerContent = this.GetInnerContent(xPathNodeIterators, "value");
                            if (innerContent == null)
                            {
                                continue;
                            }
                            innerContent = Path.Combine(this.uiPath, innerContent);
                            this.elementlist[this.elementlist.Count - 1].ImageForeground = new Bitmap(innerContent);
                            this.elementlist[this.elementlist.Count - 1].isDisplayFore = true;
                        }
                        else
                        {
                            innerContent = this.GetInnerContent(xPathNodeIterators, "value");
                            innerContent = Path.Combine(this.uiPath, innerContent);
                            this.elementlist[this.elementlist.Count - 1].ImageForeground = new Bitmap(innerContent);
                            num.Width = Convert.ToInt32(this.GetInnerContent(xPathNodeIterators, "Widthbar"));
                            num.Height = Convert.ToInt32(this.GetInnerContent(xPathNodeIterators, "Heightbar"));
                            this.elementlist[this.elementlist.Count - 1].Rect2.Size = num;
                            this.elementlist[this.elementlist.Count - 1].isDisplayFore = true;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                Environment.Exit(0);
            }
        }

        public void MouseDown(object sender, MouseEventArgs e)
        {
            bool flag = false;
            foreach (UI.Element element in this.elementlist)
            {
                if (!element.Rect.Contains(e.Location))
                {
                    continue;
                }
                flag = true;
                element.isDown = true;
                string type = element.Type;
                string str = type;
                if (type != null)
                {
                    if (str == "button")
                    {
                        element.isDisplayFore = true;
                    }
                    else if (str == "radio")
                    {
                        element.isDisplayFore = true;
                    }
                }
                if (element.Exec == null)
                {
                    continue;
                }
                element.Exec(element.Name, "MouseDown");
            }
            this.myPictureBox.Invalidate();
            if (!flag)
            {
                this.isFormMoving = true;
                this.cursorPos.X = e.X;
                this.cursorPos.Y = e.Y;
            }
        }

        public void MouseMove(object sender, MouseEventArgs e)
        {
            if (this.isFormMoving)
            {
                this.myForm.Location = new Point(this.myForm.Left + e.X - this.cursorPos.X, this.myForm.Top + e.Y - this.cursorPos.Y);
                this.myForm.Refresh();
                return;
            }
            foreach (UI.Element x in this.elementlist)
            {
                if (x.Rect.Contains(e.Location) && !x.isEnter)
                {
                    x.isEnter = true;
                    if (x.isDown)
                    {
                        string type = x.Type;
                        string str = type;
                        if (type != null)
                        {
                            if (str == "button")
                            {
                                x.isDisplayFore = true;
                            }
                            else if (str == "radio")
                            {
                                x.isDisplayFore = true;
                            }
                        }
                    }
                    else if (x.Type != "custom" && x.Type != "static")
                    {
                        x.isDisplay = true;
                    }
                    else if (x.Type == "custom" && !x.isDisplayFore)
                    {
                        x.isDisplay = true;
                    }
                    if (x.Exec != null)
                    {
                        x.Exec(x.Name, "MouseEnter");
                    }
                    this.ToolTipCnt = 0;
                }
                else if (!x.Rect.Contains(e.Location) && x.isEnter)
                {
                    x.isDisplay = false;
                    x.isEnter = false;
                    string type1 = x.Type;
                    string str1 = type1;
                    if (type1 != null)
                    {
                        if (str1 == "button")
                        {
                            x.isDisplayFore = false;
                        }
                        else if (str1 == "radio")
                        {
                            if (!x.checkstate)
                            {
                                x.isDisplayFore = false;
                            }
                        }
                    }
                    if (x.Exec != null)
                    {
                        x.Exec(x.Name, "MouseLeave");
                    }
                    this.myToolTip.Hide(this.myForm);
                }
                if (!x.isDown)
                {
                    continue;
                }
                string type2 = x.Type;
                string str2 = type2;
                if (type2 == null || !(str2 == "trackbar") || e.X - x.Rect.Width / 2 < x.Rect2.X || e.X - x.Rect.Width / 2 > x.Rect2.X + x.Rect2.Width)
                {
                    continue;
                }
                x.Rect.X = e.X - x.Rect.Width / 2;
                if (x.Exec == null)
                {
                    continue;
                }
                x.Exec(x.Name, "MouseMove");
            }
            this.myPictureBox.Invalidate();
        }

        public void MouseUp(object sender, MouseEventArgs e)
        {
            this.isFormMoving = false;
            foreach (UI.Element element in this.elementlist)
            {
                if (element.Rect.Contains(e.Location))
                {
                    if (!element.isDown)
                    {
                        string type = element.Type;
                        if (type == null || !(type == "radio"))
                        {
                            element.isDisplayFore = false;
                            element.isDisplay = false;
                        }
                        else if (!element.checkstate)
                        {
                            element.isDisplayFore = false;
                            element.isDisplay = false;
                        }
                    }
                    else
                    {
                        string str = element.Type;
                        string str1 = str;
                        if (str != null)
                        {
                            if (str1 == "button")
                            {
                                element.isDisplayFore = false;
                                element.isDisplay = true;
                            }
                            else if (str1 == "check")
                            {
                                element.checkstate = !element.checkstate;
                                element.isDisplayFore = element.checkstate;
                                element.isDisplay = true;
                            }
                        }
                        if (element.Exec != null)
                        {
                            element.Exec(element.Name, "MouseUp");
                            element.isDisplay = false;
                        }
                    }
                }
                element.isDown = false;
            }
            this.myPictureBox.Invalidate();
        }

        public void Paint(object sender, PaintEventArgs e)
        {
            foreach (UI.Element element in this.elementlist)
            {
                if (!element.isDisplayFore)
                {
                    if (!element.isDisplay)
                    {
                        continue;
                    }
                    e.Graphics.DrawImage(element.ImageHighlight, element.Rect);
                    element.isDisplayFore = false;
                }
                else
                {
                    e.Graphics.DrawImage(element.ImageForeground, element.Rect);
                    element.isDisplay = false;
                }
            }
        }

        public void ShowToolTip(Point point)
        {
            if (this.ToolTipCnt != -1)
            {
                UI toolTipCnt = this;
                toolTipCnt.ToolTipCnt = toolTipCnt.ToolTipCnt + 1;
            }
            if (this.ToolTipCnt >= 5)
            {
                foreach (UI.Element element in this.elementlist)
                {
                    if (!element.isEnter || !element.hasToolTip)
                    {
                        continue;
                    }
                    this.myToolTip.Show(element.Tooltip, this.myForm, point);
                }
                this.ToolTipCnt = -1;
            }
        }

        public class Element
        {
            public string Name;

            public string Tooltip;

            public string Type;

            public Bitmap ImageHighlight;

            public Bitmap ImageForeground;

            public Rectangle Rect;

            public Rectangle Rect2;

            public bool checkstate;

            public bool isDisplay;

            public bool isDisplayFore;

            public bool isDown;

            public bool isEnter;

            public bool hasToolTip;

            public UI.FunctionPointer Exec;

            public Element()
            {
            }
        }

        public delegate void FunctionPointer(string sender, string type);
    }
}