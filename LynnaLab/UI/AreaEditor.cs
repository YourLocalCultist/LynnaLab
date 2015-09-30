﻿using System;
using System.Drawing;
using Gtk;

namespace LynnaLab
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class AreaEditor : Gtk.Bin
    {
        Project Project {
            get { return area.Project; }
        }

        Area area;

        internal SubTileViewer subTileViewer;
        internal SubTileEditor subTileEditor;
        internal GfxViewer subTileGfxViewer;

        public AreaEditor(Area a)
        {
            this.Build();

            subTileGfxViewer = new GfxViewer();
            subTileGfxViewer.TileSelectedEvent += delegate(object sender) {
                if (subTileEditor != null)
                    subTileEditor.TileIndex = (byte)(subTileGfxViewer.SelectedIndex^0x80);
            };
            subTileGfxContainer.Add(subTileGfxViewer);

            subTileViewer = new SubTileViewer();
            subTileEditor = new SubTileEditor(this);
            subTileContainer.Add(subTileEditor);

            SetArea(a);

            areaSpinButton.Adjustment.Upper = 0x66;
            uniqueGfxComboBox.SetConstantsMapping(Project.UniqueGfxMapping);
            mainGfxComboBox.SetConstantsMapping(Project.MainGfxMapping);
            palettesComboBox.SetConstantsMapping(Project.PaletteHeaderMapping);
            tilesetSpinButton.Adjustment.Upper = 0x32;
            layoutGroupSpinButton.Adjustment.Upper = 5;
            animationsSpinButton.Adjustment.Upper = 0x15;
            animationsSpinButton.Adjustment.Lower = -1;

            SetArea(a);
        }

        void SetArea(Area a) {
            Area.TileModifiedHandler handler = delegate(int tile) {
                if (tile == subTileViewer.TileIndex) {
                    subTileViewer.QueueDraw();
                }
            };

            if (area != null)
                area.TileModifiedEvent -= handler;
            a.TileModifiedEvent += handler;

            area = a;
            subTileViewer.SetArea(area);
            if (area != null) {
                subTileGfxViewer.SetGraphicsState(area.GraphicsState, 0x2000, 0x3000);
            }

            area.DrawInvalidatedTiles = true;

            areaviewer1.SetArea(area);

            areaviewer1.TileSelectedEvent += delegate(object sender) {
                subTileViewer.TileIndex = areaviewer1.SelectedIndex;
            };

            areaSpinButton.Value = area.Index;
            SetFlags1(a.Flags1);
            SetFlags2(a.Flags2);
            SetUniqueGfx(a.UniqueGfxString);
            SetMainGfx(a.MainGfxString);
            SetPaletteHeader(a.PaletteHeaderString);
            SetTileset(a.TilesetIndex);
            SetLayoutGroup(a.LayoutGroup);
            SetAnimation(a.AnimationIndex);
        }

        void SetFlags1(int value) {
            flags1SpinButton.Value = value;
            area.Flags1 = value;
        }
        void SetFlags2(int value) {
            flags2SpinButton.Value = value;
            area.Flags2 = value;
        }
        void SetUniqueGfx(string value) {
            try {
                uniqueGfxComboBox.Active = Project.UniqueGfxMapping.IndexOf(value);
                area.UniqueGfxString = value;
            }
            catch (FormatException) {
            }
        }
        void SetMainGfx(string value) {
            try {
                mainGfxComboBox.Active = Project.MainGfxMapping.IndexOf(value);
                area.MainGfxString = value;
            }
            catch (FormatException) {
            }
        }
        void SetPaletteHeader(string value) {
            try {
                palettesComboBox.Active = Project.PaletteHeaderMapping.IndexOf(value);
                area.PaletteHeaderString = value;
            }
            catch (FormatException) {
            }
        }
        void SetTileset(int value) {
            tilesetSpinButton.Value = value;
            area.TilesetIndex = value;
        }
        void SetLayoutGroup(int value) {
            layoutGroupSpinButton.Value = value;
            area.LayoutGroup = value;
        }
        void SetAnimation(int value) {
            if (value == 0xff)
                value = -1;
            animationsSpinButton.Value = value;
            area.AnimationIndex = value;
        }

        protected void OnOkButtonClicked(object sender, EventArgs e)
        {
            Parent.Hide();
            Parent.Destroy();
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

        protected void OnAreaSpinButtonValueChanged(object sender, EventArgs e)
        {
            SpinButton button = sender as SpinButton;
            SetArea(Project.GetIndexedDataType<Area>(button.ValueAsInt));
        }

        protected void OnUniqueGfxComboBoxChanged(object sender, EventArgs e) {
            var comboBox = sender as ComboBox;
            if (comboBox.ActiveText != null)
                SetUniqueGfx(comboBox.ActiveText);
        }

        protected void OnMainGfxComboBoxChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox.ActiveText != null)
                SetMainGfx(comboBox.ActiveText);
        }

        protected void OnPalettesComboBoxChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox.ActiveText != null)
                SetPaletteHeader(comboBox.ActiveText);
        }

        protected void OnTilesetSpinButtonValueChanged(object sender, EventArgs e)
        {
            SetTileset(tilesetSpinButton.ValueAsInt);
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

    class SubTileViewer : TileGridSelector {

        int _tileIndex;
        Area area;

        public delegate void TileChangedHandler();
        public event TileChangedHandler TileChangedEvent;
        public event TileChangedHandler SubTileChangedEvent;

        public int TileIndex {
            get { return _tileIndex; }
            set {
                if (_tileIndex != value) {
                    _tileIndex = value;
                    if (TileChangedEvent != null)
                        TileChangedEvent();
                    QueueDraw();
                }
            }
        }
        public byte SubTileFlags {
            get {
                if (area == null)
                    return 0;
                return area.GetSubTileFlags(TileIndex, SelectedX, SelectedY);
            } set {
                if (area == null)
                    return;
                area.SetSubTileFlags(TileIndex, SelectedX, SelectedY, value);
            }
        }
        public byte SubTileIndex {
            get {
                if (area == null)
                    return 0;
                return area.GetSubTileIndex(TileIndex, SelectedX, SelectedY);
            } set {
                if (area == null)
                    return;
                area.SetSubTileIndex(TileIndex, SelectedX, SelectedY, value);
            }
        }

        override protected Bitmap Image {
            get {
                if (area == null)
                    return null;
                return area.GetTileImage(TileIndex);
            }
        }

        public SubTileViewer() : base() {
            Width = 2;
            Height = 2;
            TileWidth = 8;
            TileHeight = 8;
            Scale = 2;

            TileSelectedEvent += delegate(object sender) {
                SubTileChangedEvent();
            };
        }

        public void SetArea(Area a) {
            area = a;
            TileChangedEvent();
        }
    }

    class SubTileEditor : Gtk.Alignment {

        public byte TileIndex {
            get { return (byte)(subTileSpinButton.ValueAsInt); }
            set {
                subTileSpinButton.Value = value;
            }
        }

        SubTileViewer viewer;
        AreaEditor areaEditor;

        SpinButton subTileSpinButton;
        SpinButton paletteSpinButton;
        CheckButton flipXCheckButton, flipYCheckButton;
        CheckButton priorityCheckButton;
        CheckButton bankCheckButton;

        public SubTileEditor(AreaEditor areaEditor) : base(0,0,0,0) {
            this.areaEditor = areaEditor;

            viewer = areaEditor.subTileViewer;

            viewer.TileChangedEvent += delegate() {
                PullFlags();
            };
            viewer.SubTileChangedEvent += delegate() {
                PullFlags();
            };

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

            Tooltips tooltips = new Tooltips();
            tooltips.SetTip(paletteLabel, "Palette index (0-7)", null);
            tooltips.SetTip(paletteSpinButton, "Palette index (0-7)", null);

            tooltips.SetTip(priorityLabel, "Check to make colors 1-3 appear above sprites", null);
            tooltips.SetTip(priorityCheckButton, "Check to make colors 1-3 appear above sprites", null);

            tooltips.SetTip(bankLabel, "You're better off leaving this checked.", null);
            tooltips.SetTip(bankCheckButton, "You're better off leaving this checked.", null);

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

            Gtk.VBox vbox = new VBox(false, 2);

            Alignment hAlign = new Alignment(0.5f, 0, 0, 0);
            hAlign.Add(viewer);

            vbox.Spacing = 10;
            vbox.PackStart(hAlign);
            vbox.PackStart(table);
            this.Add(vbox);

            ShowAll();

            PullFlags();
        }

        void PullFlags() {
            byte flags = viewer.SubTileFlags;

            subTileSpinButton.Value = viewer.SubTileIndex;
            areaEditor.subTileGfxViewer.SelectedIndex = viewer.SubTileIndex^0x80;

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

            viewer.SubTileFlags = flags;
            viewer.SubTileIndex = (byte)(subTileSpinButton.ValueAsInt);
            areaEditor.subTileGfxViewer.SelectedIndex = viewer.SubTileIndex^0x80;
        }
    }
}
