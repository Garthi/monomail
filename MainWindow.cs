using System;
using Gtk;

public partial class MainWindow: Gtk.Window
{
	// list vars
	protected Gtk.ListStore HeaderList = new Gtk.ListStore (typeof(string), typeof(string));
	protected Gtk.ListStore FolderList = new Gtk.ListStore (typeof(string), typeof(string));

	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		//Gtk.ListStore HeaderList = new Gtk.ListStore (typeof(string), typeof(string));

		Build ();

		this.createTestData ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnTestButtonClicked (object sender, EventArgs e)
	{
		MessageDialog myMsgDia = new MessageDialog (
			this, DialogFlags.Modal, MessageType.Info, ButtonsType.Close, "Test window"
		);

		string host = ""; // <-- add a mail host for tests
		monomail.Imap cImap = new monomail.Imap(host);

		//myMsgDia.Text = cImap.getResult();

		ResponseType response = (ResponseType) myMsgDia.Run();
		if (response == ResponseType.Close || response == ResponseType.DeleteEvent) {
			myMsgDia.Destroy();
		}

	}

	protected void OnStopActionActivated (object sender, EventArgs e)
	{
		Application.Quit ();
	}

	protected void createTestData()
	{
		// create Test Folder

		// create Test Mails

		// create column 1
		Gtk.TreeViewColumn columnOne = new Gtk.TreeViewColumn ();
		columnOne.Title = "One";

		// create Column One Text
		Gtk.CellRendererText columnOneCell = new Gtk.CellRendererText ();
		columnOneCell.Visible = false;

		// add the cell to the column
		columnOne.PackStart (columnOneCell, true);

		// create column 2
		Gtk.TreeViewColumn columnTwo = new Gtk.TreeViewColumn ();
		columnTwo.Title = "Two";

		// create Column One Text
		Gtk.CellRendererText columnTwoCell = new Gtk.CellRendererText ();

		// add the cell to the column
		columnTwo.PackStart (columnTwoCell, true);

		// add the two columns
		this.treeview1.AppendColumn (columnOne);
		this.treeview1.AppendColumn (columnTwo);

		columnOne.AddAttribute (columnOneCell, "text", 0);
		columnTwo.AddAttribute (columnTwoCell, "text", 1);

		// create the list
		Gtk.ListStore HeaderList2 = new Gtk.ListStore (typeof(string), typeof(string));
		HeaderList2.AppendValues ("1", "test1 data");
		HeaderList2.AppendValues ("2", "text2 data");

		this.treeview1.Model = HeaderList2;
	}
}

