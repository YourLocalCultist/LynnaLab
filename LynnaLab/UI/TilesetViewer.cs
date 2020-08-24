﻿using System;
using System.Drawing;
using System.Collections.Generic;
using Gtk;

namespace LynnaLab
{
    [System.ComponentModel.ToolboxItem(true)]
    public class TilesetViewer : TileGridViewer
    {
        public Project Project {
            get {
                if (tileset == null)
                    return null;
                return tileset.Project;
            }
        }

        public Tileset Tileset
        {
            get { return tileset; }
        }

        protected override Bitmap Image {
            get {
                if (Tileset == null)
                    return null;
                return Tileset.GetFullCachedImage();
            }
        }

        Tileset tileset;

        public TilesetViewer() : base()
        {
            TileWidth = 16;
            TileHeight = 16;
            Width = 16;
            Height = 16;
            Selectable = true;
            SelectedIndex = 0;
        }

        public void SetTileset(Tileset t) {
            if (tileset != null)
                tileset.TileModifiedEvent -= ModifiedTileCallback;
            t.TileModifiedEvent += ModifiedTileCallback;

            tileset = t;

            tileset.ResetAnimation();
            tileset.DrawAllTiles();

            this.QueueDraw();
        }

        void ModifiedTileCallback(object sender, int tile) {
            QueueDraw();
        }

        protected override bool OnButtonPressEvent(Gdk.EventButton ev)
        {
            // Insert button press handling code here.
            return base.OnButtonPressEvent(ev);
        }

        protected override void OnSizeAllocated(Gdk.Rectangle allocation)
        {
            base.OnSizeAllocated(allocation);
            // Insert layout code here.
        }
    }
}
