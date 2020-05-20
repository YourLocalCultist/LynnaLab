﻿using System;
using System.Drawing;
using Gtk;

namespace LynnaLab
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class TilesetEditor : Gtk.Bin
    {
        Project Project {
            get { return tileset.Project; }
        }

        public Tileset Tileset {
            get {
                return tileset;
            }
        }

        Tileset tileset;

        internal SubTileEditor subTileEditor;
        internal GfxViewer subTileGfxViewer;

        public TilesetEditor(Tileset t)
        {
            this.Build();

            subTileGfxViewer = new GfxViewer();
            subTileGfxViewer.AddTileSelectedHandler(delegate(object sender, int index) {
                if (subTileEditor != null)
                    subTileEditor.SubTileIndex = (byte)(index^0x80);
            });
            subTileGfxContainer.Add(subTileGfxViewer);

            subTileEditor = new SubTileEditor(this);
            subTileContainer.Add(subTileEditor);

            SetTileset(t);

            tilesetSpinButton.Adjustment.Upper = 0x66;
            uniqueGfxComboBox.SetConstantsMapping(Project.UniqueGfxMapping);
            mainGfxComboBox.SetConstantsMapping(Project.MainGfxMapping);
            palettesComboBox.SetConstantsMapping(Project.PaletteHeaderMapping);
            tilesetLayoutSpinButton.Adjustment.Upper = 0x32;
            layoutGroupSpinButton.Adjustment.Upper = 5;
            animationsSpinButton.Adjustment.Upper = 0x15;
            animationsSpinButton.Adjustment.Lower = -1;

            SetTileset(t);
        }

        void TileModifiedHandler(int tile) {
            if (tile == subTileEditor.subTileViewer.TileIndex) {
                subTileEditor.OnTileModified();
            }
        }

        void SetTileset(Tileset t) {
            if (tileset != null)
                tileset.TileModifiedEvent -= TileModifiedHandler;
            t.TileModifiedEvent += TileModifiedHandler;

            tileset = t;
            subTileEditor.SetTileset(tileset);
            if (tileset != null) {
                subTileGfxViewer.SetGraphicsState(tileset.GraphicsState, 0x2000, 0x3000);
            }

            tileset.DrawInvalidatedTiles = true;

            tilesetviewer1.SetTileset(tileset);

            tilesetviewer1.AddTileSelectedHandler(delegate(object sender, int index) {
                subTileEditor.SetTileIndex(index);
            });

            tilesetSpinButton.Value = tileset.Index;
            SetFlags1(t.Flags1);
            SetFlags2(t.Flags2);
            SetUniqueGfx(t.UniqueGfx);
            SetMainGfx(t.MainGfx);
            SetPaletteHeader(t.PaletteHeader);
            SetTilesetLayout(t.TilesetLayoutIndex);
            SetLayoutGroup(t.LayoutGroup);
            SetAnimation(t.AnimationIndex);
        }

        void SetFlags1(int value) {
            flags1SpinButton.Value = value;
            tileset.Flags1 = value;
        }
        void SetFlags2(int value) {
            flags2SpinButton.Value = value;
            tileset.Flags2 = value;
        }
        void SetUniqueGfx(int value) {
            try {
                uniqueGfxComboBox.ActiveValue = value;
                tileset.UniqueGfx = value;
            }
            catch (FormatException) {
            }
        }
        void SetMainGfx(int value) {
            try {
                mainGfxComboBox.ActiveValue = value;
                tileset.MainGfx = value;
            }
            catch (FormatException) {
            }
        }
        void SetPaletteHeader(int value) {
            try {
                palettesComboBox.ActiveValue = value;
                tileset.PaletteHeader = value;
            }
            catch (FormatException) {
            }
        }
        void SetTilesetLayout(int value) {
            tilesetLayoutSpinButton.Value = value;
            tileset.TilesetLayoutIndex = value;
        }
        void SetLayoutGroup(int value) {
            layoutGroupSpinButton.Value = value;
            tileset.LayoutGroup = value;
        }
        void SetAnimation(int value) {
            if (value == 0xff)
                value = -1;
            animationsSpinButton.Value = value;
            tileset.AnimationIndex = value;
        }

        protected void OnOkButtonClicked(object sender, EventArgs e)
        {
            Parent.Hide();
            Parent.Dispose();
        }

        protected void OnFlags1SpinButtonValueChanged(object sender, EventArgs e)
        {
            SpinButton button = sender as SpinButton;
            SetFlags1(button.ValueAsInt);
        }

        protected void OnFlags2SpinButtonValueChanged(object sender, EventArgs e)
        {
            SpinButton button = sender as SpinButton;
            SetFlags2(button.ValueAsInt);
        }

        protected void OnTilesetSpinButtonValueChanged(object sender, EventArgs e)
        {
            SpinButton button = sender as SpinButton;
            SetTileset(Project.GetIndexedDataType<Tileset>(button.ValueAsInt));
        }

        protected void OnUniqueGfxComboBoxChanged(object sender, EventArgs e) {
            var comboBox = sender as ComboBoxFromConstants;
            if (comboBox.ActiveValue != -1)
                SetUniqueGfx(comboBox.ActiveValue);
        }

        protected void OnMainGfxComboBoxChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBoxFromConstants;
            if (comboBox.ActiveValue != -1)
                SetMainGfx(comboBox.ActiveValue);
        }

        protected void OnPalettesComboBoxChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBoxFromConstants;
            if (comboBox.ActiveValue != -1)
                SetPaletteHeader(comboBox.ActiveValue);
        }

        protected void OnTilesetLayoutSpinButtonValueChanged(object sender, EventArgs e)
        {
            SetTilesetLayout(tilesetLayoutSpinButton.ValueAsInt);
        }

        protected void OnLayoutGroupSpinButtonValueChanged(object sender, EventArgs e)
        {
            SetLayoutGroup(layoutGroupSpinButton.ValueAsInt);
        }

        protected void OnAnimationsSpinButtonValueChanged(object sender, EventArgs e)
        {
            SetAnimation(animationsSpinButton.ValueAsInt);
        }
    }

    // Draws the tile with the ability to select the quadrant to change the
    // properties.
    class SubTileViewer : TileGridViewer {

        int _tileIndex;
        Tileset tileset;

        public delegate void TileChangedHandler();

        // Called when subtile properties are modified
        public event TileChangedHandler SubTileChangedEvent;

        public int TileIndex {
            get { return _tileIndex; }
            set {
                if (_tileIndex != value) {
                    _tileIndex = value;
                    QueueDraw();
                }
            }
        }
        public byte SubTileFlags {
            get {
                if (tileset == null)
                    return 0;
                return tileset.GetSubTileFlags(TileIndex, SelectedX, SelectedY);
            } set {
                if (tileset == null)
                    return;
                tileset.SetSubTileFlags(TileIndex, SelectedX, SelectedY, value);
            }
        }
        public byte SubTileIndex {
            get {
                if (tileset == null)
                    return 0;
                return tileset.GetSubTileIndex(TileIndex, SelectedX, SelectedY);
            } set {
                if (tileset == null)
                    return;
                tileset.SetSubTileIndex(TileIndex, SelectedX, SelectedY, value);
            }
        }

        override protected Bitmap Image {
            get {
                if (tileset == null)
                    return null;
                return tileset.GetTileImage(TileIndex);
            }
        }

        public SubTileViewer() : base() {
            Width = 2;
            Height = 2;
            TileWidth = 8;
            TileHeight = 8;
            Scale = 2;
            Selectable = true;
            SelectedIndex = 0;

            base.AddTileSelectedHandler(delegate(object sender, int index) {
                SubTileChangedEvent();
            });
        }

        public void SetTileset(Tileset t) {
            tileset = t;
        }
    }

    // Draws the tile with red rectangle representing solidity.
    class SubTileCollisionEditor : TileGridViewer {

        int _tileIndex;
        Tileset tileset;

        public event System.Action CollisionsChangedHandler;

        public int TileIndex {
            get { return _tileIndex; }
            set {
                if (_tileIndex != value) {
                    _tileIndex = value;
                    QueueDraw();
                }
            }
        }

        override protected Bitmap Image {
            get {
                if (tileset == null)
                    return null;
                Bitmap image = new Bitmap(tileset.GetTileImage(TileIndex));
                Graphics g = Graphics.FromImage(image);

                // Made solid tiles red
                for (int i=0; i<4; i++) {
                    int x=i%2;
                    int y=i/2;
                    if ((tileset.GetTileCollision(TileIndex)&0xf0) == 0) {
                        if (GetBasicCollision(TileIndex, x, y)) {
                            Color c = Color.FromArgb(0x80, 255, 0, 0);
                            g.FillRectangle(new SolidBrush(c), x*8,y*8,8,8);
                        }
                    }
                }

                g.Dispose();

                return image;
            }
        }

        public SubTileCollisionEditor() : base() {
            Width = 2;
            Height = 2;
            TileWidth = 8;
            TileHeight = 8;
            Scale = 2;

            // On clicked...
            TileGridEventHandler callback = delegate(object sender, int index) {
                // Toggle the collision of the subtile if it uses the
                // "basic" collision mode (upper nibble is zero).
                if ((tileset.GetTileCollision(TileIndex)&0xf0) == 0) {
                    SetBasicCollision(TileIndex, HoveringX, HoveringY,
                            !GetBasicCollision(TileIndex, HoveringX, HoveringY));
                }
            };

            base.AddMouseAction(MouseButton.Any, MouseModifier.Any | MouseModifier.Drag, GridAction.Callback, callback);
        }

        bool GetBasicCollision(int subTile, int x, int y) {
            return tileset.GetSubTileBasicCollision(subTile, x, y);
        }

        void SetBasicCollision(int subTile, int x, int y, bool val) {
            tileset.SetSubTileBasicCollision(subTile, x, y, val);
            if (CollisionsChangedHandler != null)
                CollisionsChangedHandler();
        }

        public void SetTileset(Tileset t) {
            tileset = t;
        }
    }

    class SubTileEditor : Gtk.Alignment {

        // Which tile is being edited
        public byte TileIndex {
            get {
                return (byte)subTileViewer.TileIndex;
            }
        }
        // Which subtile makes up the selected part of the tile
        public byte SubTileIndex {
            get { return (byte)(subTileSpinButton.ValueAsInt); }
            set {
                subTileSpinButton.Value = value;
            }
        }

        Tileset Tileset {
            get {
                return tilesetEditor.Tileset;
            }
        }

        internal SubTileViewer subTileViewer;
        SubTileCollisionEditor subTileCollisionEditor;
        TilesetEditor tilesetEditor;

        SpinButton collisionSpinButton;

        SpinButton subTileSpinButton;
        SpinButton paletteSpinButton;
        CheckButton flipXCheckButton, flipYCheckButton;
        CheckButton priorityCheckButton;
        CheckButton bankCheckButton;

        public SubTileEditor(TilesetEditor tilesetEditor) : base(0,0,0,0) {
            this.tilesetEditor = tilesetEditor;

            Gtk.Box tmpBox;

            Gtk.VBox vbox = new VBox(false, 2);
            vbox.Spacing = 10;

            // Top row: 2 images of the tile, one for selecting, one to show
            // collisions

            subTileViewer = new SubTileViewer();

            subTileViewer.SubTileChangedEvent += delegate() {
                PullEverything();
            };

            subTileCollisionEditor = new SubTileCollisionEditor();
            subTileCollisionEditor.CollisionsChangedHandler += () => {
                PullEverything();
            };

            Alignment hAlign = new Alignment(0.5f, 0, 0, 0);
            Gtk.HBox hbox = new HBox(false, 2);
            hbox.Add(subTileViewer);
            hbox.Add(subTileCollisionEditor);
            hAlign.Add(hbox);

            vbox.Add(hAlign);

            // Next row: collision value

            collisionSpinButton = new SpinButtonHexadecimal(0,255);
            collisionSpinButton.Digits = 2;
            collisionSpinButton.CanFocus = false;
            collisionSpinButton.ValueChanged += delegate(object sender, EventArgs e) {
                Tileset.SetTileCollision(TileIndex, (byte)collisionSpinButton.ValueAsInt);
                subTileCollisionEditor.QueueDraw();
            };

            Gtk.Label collisionLabel = new Gtk.Label("Collisions");

            tmpBox = new Gtk.HBox(false, 2);
            tmpBox.Add(collisionLabel);
            tmpBox.Add(collisionSpinButton);
            vbox.Add(tmpBox);

            // Next rows: subtile properties

            var table = new Table(2, 2, false);
            table.ColumnSpacing = 6;
            table.RowSpacing = 6;

            subTileSpinButton = new SpinButtonHexadecimal(0,255);
            subTileSpinButton.CanFocus = false;
            subTileSpinButton.ValueChanged += delegate(object sender, EventArgs e) {
                PushFlags();
            };
            paletteSpinButton = new SpinButton(0,7,1);
            paletteSpinButton.CanFocus = false;
            paletteSpinButton.ValueChanged += delegate(object sender, EventArgs e) {
                PushFlags();
            };
            flipXCheckButton = new Gtk.CheckButton();
            flipXCheckButton.CanFocus = false;
            flipXCheckButton.Toggled += delegate(object sender, EventArgs e) {
                PushFlags();
            };
            flipYCheckButton = new Gtk.CheckButton();
            flipYCheckButton.CanFocus = false;
            flipYCheckButton.Toggled += delegate(object sender, EventArgs e) {
                PushFlags();
            };
            priorityCheckButton = new Gtk.CheckButton();
            priorityCheckButton.CanFocus = false;
            priorityCheckButton.Toggled += delegate(object sender, EventArgs e) {
                PushFlags();
            };
            bankCheckButton = new Gtk.CheckButton();
            bankCheckButton.CanFocus = false;
            bankCheckButton.Toggled += delegate(object sender, EventArgs e) {
                PushFlags();
            };

            Gtk.Label subTileLabel = new Gtk.Label("Subtile Index");
            Gtk.Label paletteLabel = new Gtk.Label("Palette");
            Gtk.Label flipXLabel = new Gtk.Label("Flip X");
            Gtk.Label flipYLabel = new Gtk.Label("Flip Y");
            Gtk.Label priorityLabel = new Gtk.Label("Priority");
            Gtk.Label bankLabel = new Gtk.Label("Bank (0/1)");

            paletteLabel.TooltipText = "Palette index (0-7)";
            paletteSpinButton.TooltipText = "Palette index (0-7)";

            priorityLabel.TooltipText = "Check to make colors 1-3 appear above sprites";
            priorityCheckButton.TooltipText = "Check to make colors 1-3 appear above sprites";

            bankLabel.TooltipText = "You're better off leaving this checked.";
            bankCheckButton.TooltipText = "You're better off leaving this checked.";

            uint y = 0;

            table.Attach(subTileLabel, 0, 1, y, y+1);
            table.Attach(subTileSpinButton, 1, 2, y, y+1);
            y++;
            table.Attach(paletteLabel, 0, 1, y, y+1);
            table.Attach(paletteSpinButton, 1, 2, y, y+1);
            y++;
            table.Attach(flipXLabel, 0, 1, y, y+1);
            table.Attach(flipXCheckButton, 1, 2, y, y+1);
            y++;
            table.Attach(flipYLabel, 0, 1, y, y+1);
            table.Attach(flipYCheckButton, 1, 2, y, y+1);
            y++;
            table.Attach(priorityLabel, 0, 1, y, y+1);
            table.Attach(priorityCheckButton, 1, 2, y, y+1);
            y++;
            table.Attach(bankLabel, 0, 1, y, y+1);
            table.Attach(bankCheckButton, 1, 2, y, y+1);
            y++;

            vbox.Add(table);
            this.Add(vbox);

            ShowAll();

            PullEverything();
        }

        public void SetTileset(Tileset t) {
            subTileViewer.SetTileset(t);
            subTileCollisionEditor.SetTileset(t);
            PullEverything();
        }

        public void SetTileIndex(int tile) {
            subTileViewer.TileIndex = tile;
            subTileCollisionEditor.TileIndex = tile;
            PullEverything();
        }

        public void OnTileModified() {
            subTileViewer.QueueDraw();
            subTileCollisionEditor.QueueDraw();
        }

        void PullEverything() {
            if (Tileset != null) {
                collisionSpinButton.Value = Tileset.GetTileCollision(TileIndex);
                subTileCollisionEditor.QueueDraw();
            }

            byte flags = subTileViewer.SubTileFlags;

            subTileSpinButton.Value = subTileViewer.SubTileIndex;
            tilesetEditor.subTileGfxViewer.SelectedIndex = subTileViewer.SubTileIndex^0x80;

            paletteSpinButton.Value = flags&7;
            flipXCheckButton.Active = ((flags & 0x20) != 0);
            flipYCheckButton.Active = ((flags & 0x40) != 0);
            priorityCheckButton.Active = ((flags & 0x80) != 0);
            bankCheckButton.Active = ((flags & 0x08) != 0);
        }

        void PushFlags() {
            byte flags = 0;
            flags |= (byte)paletteSpinButton.ValueAsInt;
            if (flipXCheckButton.Active)
                flags |= 0x20;
            if (flipYCheckButton.Active)
                flags |= 0x40;
            if (priorityCheckButton.Active)
                flags |= 0x80;
            if (bankCheckButton.Active)
                flags |= 0x08;

            subTileViewer.SubTileFlags = flags;
            subTileViewer.SubTileIndex = (byte)(subTileSpinButton.ValueAsInt);
            tilesetEditor.subTileGfxViewer.SelectedIndex = subTileViewer.SubTileIndex^0x80;
        }
    }
}
