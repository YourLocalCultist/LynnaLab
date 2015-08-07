
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.UIManager UIManager;
	
	private global::Gtk.Action FileAction;
	
	private global::Gtk.Action OpenAction;
	
	private global::Gtk.Action QuitAction;
	
	private global::Gtk.Action SaveAction;
	
	private global::Gtk.Action ViewAction;
	
	private global::Gtk.ToggleAction AnimationsAction;
	
	private global::Gtk.VBox vbox1;
	
	private global::Gtk.MenuBar menubar1;
	
	private global::Gtk.HBox hbox3;
	
	private global::Gtk.HBox hbox1;
	
	private global::Gtk.VBox vbox5;
	
	private global::Gtk.Table table4;
	
	private global::Gtk.Label label3;
	
	private global::LynnaLab.SpinButtonHexadecimal roomSpinButton;
	
	private global::Gtk.Frame frame1;
	
	private global::Gtk.Alignment GtkAlignment;
	
	private global::Gtk.Table table3;
	
	private global::LynnaLab.SpinButtonHexadecimal areaSpinButton;
	
	private global::Gtk.Label label4;
	
	private global::Gtk.Label label8;
	
	private global::Gtk.SpinButton musicSpinButton;
	
	private global::Gtk.Label GtkLabel2;
	
	private global::Gtk.VBox vbox2;
	
	private global::LynnaLab.AreaViewer areaviewer1;
	
	private global::LynnaLab.RoomEditor roomeditor1;
	
	private global::Gtk.Notebook notebook2;
	
	private global::Gtk.VBox vbox4;
	
	private global::Gtk.Table table2;
	
	private global::Gtk.Label label7;
	
	private global::Gtk.SpinButton worldSpinButton;
	
	private global::LynnaLab.Minimap worldMinimap;
	
	private global::Gtk.Label label5;
	
	private global::Gtk.VBox vbox3;
	
	private global::Gtk.Table table1;
	
	private global::Gtk.SpinButton dungeonSpinButton;
	
	private global::Gtk.SpinButton floorSpinButton;
	
	private global::Gtk.Label label1;
	
	private global::Gtk.Label label2;
	
	private global::LynnaLab.Minimap dungeonMinimap;
	
	private global::Gtk.Label label6;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.UIManager = new global::Gtk.UIManager ();
		global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup ("Default");
		this.FileAction = new global::Gtk.Action ("FileAction", global::Mono.Unix.Catalog.GetString ("_File"), null, null);
		this.FileAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_File");
		w1.Add (this.FileAction, "<Alt>f");
		this.OpenAction = new global::Gtk.Action ("OpenAction", global::Mono.Unix.Catalog.GetString ("_Open"), null, null);
		this.OpenAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Open");
		w1.Add (this.OpenAction, "<Primary>o");
		this.QuitAction = new global::Gtk.Action ("QuitAction", global::Mono.Unix.Catalog.GetString ("_Quit"), null, null);
		this.QuitAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Quit");
		w1.Add (this.QuitAction, "<Primary>q");
		this.SaveAction = new global::Gtk.Action ("SaveAction", global::Mono.Unix.Catalog.GetString ("_Save"), null, null);
		this.SaveAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Save");
		w1.Add (this.SaveAction, "<Primary>s");
		this.ViewAction = new global::Gtk.Action ("ViewAction", global::Mono.Unix.Catalog.GetString ("_View"), null, null);
		this.ViewAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_View");
		w1.Add (this.ViewAction, "<Alt>f");
		this.AnimationsAction = new global::Gtk.ToggleAction ("AnimationsAction", global::Mono.Unix.Catalog.GetString ("_Animations"), null, null);
		this.AnimationsAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Animations");
		w1.Add (this.AnimationsAction, "<Primary>a");
		this.UIManager.InsertActionGroup (w1, 0);
		this.AddAccelGroup (this.UIManager.AccelGroup);
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("Lynna Lab");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.vbox1 = new global::Gtk.VBox ();
		this.vbox1.Name = "vbox1";
		this.vbox1.Spacing = 6;
		// Container child vbox1.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString ("<ui><menubar name='menubar1'><menu name='FileAction' action='FileAction'><menuitem name='OpenAction' action='OpenAction'/><menuitem name='SaveAction' action='SaveAction'/><menuitem name='QuitAction' action='QuitAction'/></menu><menu name='ViewAction' action='ViewAction'><menuitem name='AnimationsAction' action='AnimationsAction'/></menu></menubar></ui>");
		this.menubar1 = ((global::Gtk.MenuBar)(this.UIManager.GetWidget ("/menubar1")));
		this.menubar1.Name = "menubar1";
		this.vbox1.Add (this.menubar1);
		global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.menubar1]));
		w2.Position = 0;
		w2.Expand = false;
		w2.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.hbox3 = new global::Gtk.HBox ();
		this.hbox3.Name = "hbox3";
		this.hbox3.Spacing = 6;
		// Container child hbox3.Gtk.Box+BoxChild
		this.hbox1 = new global::Gtk.HBox ();
		this.hbox1.Name = "hbox1";
		this.hbox1.Spacing = 6;
		// Container child hbox1.Gtk.Box+BoxChild
		this.vbox5 = new global::Gtk.VBox ();
		this.vbox5.Name = "vbox5";
		this.vbox5.Spacing = 6;
		// Container child vbox5.Gtk.Box+BoxChild
		this.table4 = new global::Gtk.Table (((uint)(1)), ((uint)(2)), false);
		this.table4.Name = "table4";
		this.table4.RowSpacing = ((uint)(6));
		this.table4.ColumnSpacing = ((uint)(6));
		// Container child table4.Gtk.Table+TableChild
		this.label3 = new global::Gtk.Label ();
		this.label3.Name = "label3";
		this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("Room");
		this.table4.Add (this.label3);
		global::Gtk.Table.TableChild w3 = ((global::Gtk.Table.TableChild)(this.table4 [this.label3]));
		w3.XOptions = ((global::Gtk.AttachOptions)(4));
		w3.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table4.Gtk.Table+TableChild
		this.roomSpinButton = new global::LynnaLab.SpinButtonHexadecimal ();
		this.roomSpinButton.CanFocus = true;
		this.roomSpinButton.Name = "roomSpinButton";
		this.roomSpinButton.Adjustment.Upper = 1535;
		this.roomSpinButton.Adjustment.PageIncrement = 10;
		this.roomSpinButton.Adjustment.StepIncrement = 1;
		this.roomSpinButton.ClimbRate = 1;
		this.roomSpinButton.Digits = ((uint)(2));
		this.roomSpinButton.Numeric = true;
		this.table4.Add (this.roomSpinButton);
		global::Gtk.Table.TableChild w4 = ((global::Gtk.Table.TableChild)(this.table4 [this.roomSpinButton]));
		w4.LeftAttach = ((uint)(1));
		w4.RightAttach = ((uint)(2));
		w4.XOptions = ((global::Gtk.AttachOptions)(4));
		w4.YOptions = ((global::Gtk.AttachOptions)(4));
		this.vbox5.Add (this.table4);
		global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox5 [this.table4]));
		w5.Position = 0;
		w5.Expand = false;
		w5.Fill = false;
		// Container child vbox5.Gtk.Box+BoxChild
		this.frame1 = new global::Gtk.Frame ();
		this.frame1.Name = "frame1";
		// Container child frame1.Gtk.Container+ContainerChild
		this.GtkAlignment = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
		this.GtkAlignment.Name = "GtkAlignment";
		this.GtkAlignment.LeftPadding = ((uint)(12));
		// Container child GtkAlignment.Gtk.Container+ContainerChild
		this.table3 = new global::Gtk.Table (((uint)(3)), ((uint)(2)), false);
		this.table3.Name = "table3";
		this.table3.RowSpacing = ((uint)(6));
		this.table3.ColumnSpacing = ((uint)(6));
		// Container child table3.Gtk.Table+TableChild
		this.areaSpinButton = new global::LynnaLab.SpinButtonHexadecimal ();
		this.areaSpinButton.CanFocus = true;
		this.areaSpinButton.Name = "areaSpinButton";
		this.areaSpinButton.Adjustment.Upper = 255;
		this.areaSpinButton.Adjustment.PageIncrement = 10;
		this.areaSpinButton.Adjustment.StepIncrement = 1;
		this.areaSpinButton.ClimbRate = 1;
		this.areaSpinButton.Numeric = true;
		this.table3.Add (this.areaSpinButton);
		global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table3 [this.areaSpinButton]));
		w6.TopAttach = ((uint)(1));
		w6.BottomAttach = ((uint)(2));
		w6.LeftAttach = ((uint)(1));
		w6.RightAttach = ((uint)(2));
		w6.XOptions = ((global::Gtk.AttachOptions)(4));
		w6.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table3.Gtk.Table+TableChild
		this.label4 = new global::Gtk.Label ();
		this.label4.Name = "label4";
		this.label4.LabelProp = global::Mono.Unix.Catalog.GetString ("Music");
		this.table3.Add (this.label4);
		global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table3 [this.label4]));
		w7.XOptions = ((global::Gtk.AttachOptions)(4));
		w7.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table3.Gtk.Table+TableChild
		this.label8 = new global::Gtk.Label ();
		this.label8.Name = "label8";
		this.label8.LabelProp = global::Mono.Unix.Catalog.GetString ("Area");
		this.table3.Add (this.label8);
		global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.table3 [this.label8]));
		w8.TopAttach = ((uint)(1));
		w8.BottomAttach = ((uint)(2));
		w8.XOptions = ((global::Gtk.AttachOptions)(4));
		w8.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table3.Gtk.Table+TableChild
		this.musicSpinButton = new global::Gtk.SpinButton (0, 100, 1);
		this.musicSpinButton.CanFocus = true;
		this.musicSpinButton.Name = "musicSpinButton";
		this.musicSpinButton.Adjustment.PageIncrement = 10;
		this.musicSpinButton.ClimbRate = 1;
		this.musicSpinButton.Numeric = true;
		this.table3.Add (this.musicSpinButton);
		global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.table3 [this.musicSpinButton]));
		w9.LeftAttach = ((uint)(1));
		w9.RightAttach = ((uint)(2));
		w9.XOptions = ((global::Gtk.AttachOptions)(4));
		w9.YOptions = ((global::Gtk.AttachOptions)(4));
		this.GtkAlignment.Add (this.table3);
		this.frame1.Add (this.GtkAlignment);
		this.GtkLabel2 = new global::Gtk.Label ();
		this.GtkLabel2.Name = "GtkLabel2";
		this.GtkLabel2.LabelProp = global::Mono.Unix.Catalog.GetString ("Room Properties");
		this.GtkLabel2.UseMarkup = true;
		this.frame1.LabelWidget = this.GtkLabel2;
		this.vbox5.Add (this.frame1);
		global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.vbox5 [this.frame1]));
		w12.Position = 1;
		this.hbox1.Add (this.vbox5);
		global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.vbox5]));
		w13.Position = 0;
		w13.Expand = false;
		w13.Fill = false;
		// Container child hbox1.Gtk.Box+BoxChild
		this.vbox2 = new global::Gtk.VBox ();
		this.vbox2.Name = "vbox2";
		this.vbox2.Spacing = 6;
		// Container child vbox2.Gtk.Box+BoxChild
		this.areaviewer1 = new global::LynnaLab.AreaViewer ();
		this.areaviewer1.WidthRequest = 256;
		this.areaviewer1.HeightRequest = 256;
		this.areaviewer1.Name = "areaviewer1";
		this.areaviewer1.SelectedIndex = 0;
		this.areaviewer1.Height = 16;
		this.areaviewer1.Width = 16;
		this.areaviewer1.TileWidth = 16;
		this.areaviewer1.TileHeight = 16;
		this.vbox2.Add (this.areaviewer1);
		global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.areaviewer1]));
		w14.Position = 0;
		w14.Expand = false;
		w14.Fill = false;
		// Container child vbox2.Gtk.Box+BoxChild
		this.roomeditor1 = new global::LynnaLab.RoomEditor ();
		this.roomeditor1.WidthRequest = 240;
		this.roomeditor1.HeightRequest = 224;
		this.roomeditor1.Name = "roomeditor1";
		this.roomeditor1.Height = 14;
		this.roomeditor1.Width = 15;
		this.roomeditor1.TileWidth = 16;
		this.roomeditor1.TileHeight = 16;
		this.vbox2.Add (this.roomeditor1);
		global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.roomeditor1]));
		w15.Position = 1;
		w15.Expand = false;
		w15.Fill = false;
		this.hbox1.Add (this.vbox2);
		global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.vbox2]));
		w16.Position = 1;
		w16.Expand = false;
		w16.Fill = false;
		this.hbox3.Add (this.hbox1);
		global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.hbox1]));
		w17.Position = 0;
		w17.Expand = false;
		w17.Fill = false;
		// Container child hbox3.Gtk.Box+BoxChild
		this.notebook2 = new global::Gtk.Notebook ();
		this.notebook2.CanFocus = true;
		this.notebook2.Name = "notebook2";
		this.notebook2.CurrentPage = 0;
		// Container child notebook2.Gtk.Notebook+NotebookChild
		this.vbox4 = new global::Gtk.VBox ();
		this.vbox4.Name = "vbox4";
		this.vbox4.Spacing = 6;
		// Container child vbox4.Gtk.Box+BoxChild
		this.table2 = new global::Gtk.Table (((uint)(1)), ((uint)(2)), false);
		this.table2.Name = "table2";
		this.table2.RowSpacing = ((uint)(6));
		this.table2.ColumnSpacing = ((uint)(6));
		// Container child table2.Gtk.Table+TableChild
		this.label7 = new global::Gtk.Label ();
		this.label7.Name = "label7";
		this.label7.Xalign = 1F;
		this.label7.LabelProp = global::Mono.Unix.Catalog.GetString ("World Index");
		this.table2.Add (this.label7);
		global::Gtk.Table.TableChild w18 = ((global::Gtk.Table.TableChild)(this.table2 [this.label7]));
		w18.XOptions = ((global::Gtk.AttachOptions)(4));
		w18.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table2.Gtk.Table+TableChild
		this.worldSpinButton = new global::Gtk.SpinButton (0, 100, 1);
		this.worldSpinButton.CanFocus = true;
		this.worldSpinButton.Name = "worldSpinButton";
		this.worldSpinButton.Adjustment.PageIncrement = 10;
		this.worldSpinButton.ClimbRate = 1;
		this.worldSpinButton.Numeric = true;
		this.table2.Add (this.worldSpinButton);
		global::Gtk.Table.TableChild w19 = ((global::Gtk.Table.TableChild)(this.table2 [this.worldSpinButton]));
		w19.LeftAttach = ((uint)(1));
		w19.RightAttach = ((uint)(2));
		w19.XOptions = ((global::Gtk.AttachOptions)(4));
		w19.YOptions = ((global::Gtk.AttachOptions)(4));
		this.vbox4.Add (this.table2);
		global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.table2]));
		w20.Position = 0;
		w20.Expand = false;
		w20.Fill = false;
		// Container child vbox4.Gtk.Box+BoxChild
		this.worldMinimap = new global::LynnaLab.Minimap ();
		this.worldMinimap.WidthRequest = 320;
		this.worldMinimap.HeightRequest = 256;
		this.worldMinimap.Name = "worldMinimap";
		this.worldMinimap.Floor = 0;
		this.worldMinimap.SelectedIndex = 0;
		this.worldMinimap.Height = 16;
		this.worldMinimap.Width = 16;
		this.worldMinimap.TileWidth = 20;
		this.worldMinimap.TileHeight = 16;
		this.vbox4.Add (this.worldMinimap);
		global::Gtk.Box.BoxChild w21 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.worldMinimap]));
		w21.Position = 1;
		w21.Expand = false;
		w21.Fill = false;
		this.notebook2.Add (this.vbox4);
		// Notebook tab
		this.label5 = new global::Gtk.Label ();
		this.label5.Name = "label5";
		this.label5.LabelProp = global::Mono.Unix.Catalog.GetString ("Worlds");
		this.notebook2.SetTabLabel (this.vbox4, this.label5);
		this.label5.ShowAll ();
		// Container child notebook2.Gtk.Notebook+NotebookChild
		this.vbox3 = new global::Gtk.VBox ();
		this.vbox3.Name = "vbox3";
		this.vbox3.Spacing = 6;
		// Container child vbox3.Gtk.Box+BoxChild
		this.table1 = new global::Gtk.Table (((uint)(2)), ((uint)(2)), false);
		this.table1.Name = "table1";
		this.table1.RowSpacing = ((uint)(6));
		this.table1.ColumnSpacing = ((uint)(6));
		// Container child table1.Gtk.Table+TableChild
		this.dungeonSpinButton = new global::Gtk.SpinButton (0, 100, 1);
		this.dungeonSpinButton.CanFocus = true;
		this.dungeonSpinButton.Name = "dungeonSpinButton";
		this.dungeonSpinButton.Adjustment.PageIncrement = 10;
		this.dungeonSpinButton.ClimbRate = 1;
		this.dungeonSpinButton.Numeric = true;
		this.table1.Add (this.dungeonSpinButton);
		global::Gtk.Table.TableChild w23 = ((global::Gtk.Table.TableChild)(this.table1 [this.dungeonSpinButton]));
		w23.LeftAttach = ((uint)(1));
		w23.RightAttach = ((uint)(2));
		w23.XOptions = ((global::Gtk.AttachOptions)(4));
		w23.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table1.Gtk.Table+TableChild
		this.floorSpinButton = new global::Gtk.SpinButton (0, 100, 1);
		this.floorSpinButton.CanFocus = true;
		this.floorSpinButton.Name = "floorSpinButton";
		this.floorSpinButton.Adjustment.PageIncrement = 10;
		this.floorSpinButton.ClimbRate = 1;
		this.floorSpinButton.Numeric = true;
		this.table1.Add (this.floorSpinButton);
		global::Gtk.Table.TableChild w24 = ((global::Gtk.Table.TableChild)(this.table1 [this.floorSpinButton]));
		w24.TopAttach = ((uint)(1));
		w24.BottomAttach = ((uint)(2));
		w24.LeftAttach = ((uint)(1));
		w24.RightAttach = ((uint)(2));
		w24.XOptions = ((global::Gtk.AttachOptions)(4));
		w24.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table1.Gtk.Table+TableChild
		this.label1 = new global::Gtk.Label ();
		this.label1.Name = "label1";
		this.label1.Xalign = 1F;
		this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("Dungeon Index");
		this.table1.Add (this.label1);
		global::Gtk.Table.TableChild w25 = ((global::Gtk.Table.TableChild)(this.table1 [this.label1]));
		w25.XOptions = ((global::Gtk.AttachOptions)(4));
		w25.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table1.Gtk.Table+TableChild
		this.label2 = new global::Gtk.Label ();
		this.label2.Name = "label2";
		this.label2.Xalign = 1F;
		this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("Floor");
		this.table1.Add (this.label2);
		global::Gtk.Table.TableChild w26 = ((global::Gtk.Table.TableChild)(this.table1 [this.label2]));
		w26.TopAttach = ((uint)(1));
		w26.BottomAttach = ((uint)(2));
		w26.XOptions = ((global::Gtk.AttachOptions)(4));
		w26.YOptions = ((global::Gtk.AttachOptions)(4));
		this.vbox3.Add (this.table1);
		global::Gtk.Box.BoxChild w27 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.table1]));
		w27.Position = 0;
		w27.Expand = false;
		w27.Fill = false;
		// Container child vbox3.Gtk.Box+BoxChild
		this.dungeonMinimap = new global::LynnaLab.Minimap ();
		this.dungeonMinimap.WidthRequest = 240;
		this.dungeonMinimap.HeightRequest = 176;
		this.dungeonMinimap.Name = "dungeonMinimap";
		this.dungeonMinimap.Floor = 0;
		this.dungeonMinimap.SelectedIndex = 0;
		this.dungeonMinimap.Height = 8;
		this.dungeonMinimap.Width = 8;
		this.dungeonMinimap.TileWidth = 30;
		this.dungeonMinimap.TileHeight = 22;
		this.vbox3.Add (this.dungeonMinimap);
		global::Gtk.Box.BoxChild w28 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.dungeonMinimap]));
		w28.Position = 1;
		w28.Expand = false;
		w28.Fill = false;
		this.notebook2.Add (this.vbox3);
		global::Gtk.Notebook.NotebookChild w29 = ((global::Gtk.Notebook.NotebookChild)(this.notebook2 [this.vbox3]));
		w29.Position = 1;
		// Notebook tab
		this.label6 = new global::Gtk.Label ();
		this.label6.Name = "label6";
		this.label6.LabelProp = global::Mono.Unix.Catalog.GetString ("Dungeons");
		this.notebook2.SetTabLabel (this.vbox3, this.label6);
		this.label6.ShowAll ();
		this.hbox3.Add (this.notebook2);
		global::Gtk.Box.BoxChild w30 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.notebook2]));
		w30.Position = 1;
		w30.Expand = false;
		w30.Fill = false;
		this.vbox1.Add (this.hbox3);
		global::Gtk.Box.BoxChild w31 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.hbox3]));
		w31.Position = 1;
		w31.Expand = false;
		w31.Fill = false;
		this.Add (this.vbox1);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 728;
		this.DefaultHeight = 546;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.OpenAction.Activated += new global::System.EventHandler (this.OnOpenActionActivated);
		this.QuitAction.Activated += new global::System.EventHandler (this.OnQuitActionActivated);
		this.SaveAction.Activated += new global::System.EventHandler (this.OnSaveActionActivated);
		this.AnimationsAction.Activated += new global::System.EventHandler (this.OnAnimationsActionActivated);
		this.roomSpinButton.ValueChanged += new global::System.EventHandler (this.OnSpinbuttonhexadecimal1ValueChanged);
		this.musicSpinButton.ValueChanged += new global::System.EventHandler (this.OnMusicSpinButtonValueChanged);
		this.areaSpinButton.ValueChanged += new global::System.EventHandler (this.OnAreaSpinButtonValueChanged);
		this.notebook2.SwitchPage += new global::Gtk.SwitchPageHandler (this.OnNotebook2SwitchPage);
		this.worldSpinButton.ValueChanged += new global::System.EventHandler (this.OnWorldSpinButtonValueChanged);
		this.floorSpinButton.ValueChanged += new global::System.EventHandler (this.OnFloorSpinButtonValueChanged);
		this.dungeonSpinButton.ValueChanged += new global::System.EventHandler (this.OnDungeonSpinButtonValueChanged);
	}
}
