using System;
using System.Drawing;
using Gtk;
using Cairo;

namespace LynnaLab {

    public class ObjectBox : TileGridViewer {
        ObjectGroup objectGroup;


        public ObjectGroup ObjectGroup { get { return objectGroup; } }


        // Constructors

        public ObjectBox(ObjectGroup group) : base() {
            objectGroup = group;

            base.Width = 8;
            base.Height = 2;
            base.TileWidth = 18;
            base.TileHeight = 18;
            base.PaddingX = 2;
            base.PaddingY = 2;

            base.Selectable = true;
            base.MaxIndex = objectGroup.GetNumObjects() - 1;

            objectGroup.ModifiedEvent += delegate(object sender, EventArgs args) {
                RedrawAll();
                base.MaxIndex = objectGroup.GetNumObjects() - 1;
            };

            TileGridEventHandler dragCallback = delegate(object sender, int index) {
                if (index != SelectedIndex) {
                    objectGroup.MoveObject(SelectedIndex, index);
                    SelectedIndex = index;
                }
            };

            base.AddMouseAction(MouseButton.Any, MouseModifier.Any | MouseModifier.Drag,
                    GridAction.Callback, dragCallback);

            RedrawAll();
        }


        // Methods

        public void SetSelectedIndex(int index) {
            SelectedIndex = index;
        }


        Bitmap TileDrawer(int index) {
            if (index >= objectGroup.GetNumObjects())
                return null;

            Bitmap bitmap = new Bitmap(18, 18);

            using (Cairo.Context cr = new BitmapContext(bitmap)) {
                ObjectDefinition obj = objectGroup.GetObject(index);
                cr.SetSourceColor(ObjectGroupEditor.GetObjectColor(obj.GetObjectType()));
                cr.Rectangle(0, 0, 18, 18);
                cr.Fill();
                cr.Rectangle(1, 1, 16, 16); // Cut off object drawing outside 16x16 area
                cr.Clip();
                DrawObject(obj, cr, 9, 9);
            }
            return bitmap;
        }

        void DrawObject(ObjectDefinition obj, Cairo.Context cr, double x, double y) {
            if (obj.GetGameObject() != null) {
                try {
                    ObjectAnimationFrame o = obj.GetGameObject().DefaultAnimation.GetFrame(0);
                    o.Draw(cr, (int)x, (int)y);
                }
                catch(NoAnimationException) {
                    // No animation defined
                }
                catch(InvalidAnimationException) {
                    // Error parsing an animation; draw a blue X to indicate the error
                    int width = 16;
                    double xPos = x-width/2 + 0.5;
                    double yPos = y-width/2 + 0.5;

                    cr.SetSourceColor(CairoHelper.ConvertColor(System.Drawing.Color.Blue));
                    cr.MoveTo(xPos, yPos);
                    cr.LineTo(xPos+width-1, yPos+width-1);
                    cr.MoveTo(xPos+width-1, yPos);
                    cr.LineTo(xPos, yPos+width-1);
                    cr.Stroke();
                }
            }
        }

        void RedrawAll() {
            base.DrawImageWithTiles(this.TileDrawer);
        }
    }
}

