using System.Text;
using EntityFrame.API.Commands;
using EntityFrame.API.Data;
using EntityFrame.API.Models;
using ComEngineers.DataProcessing;
using TypesOfData = EntityFrame.API.Commands.TypesOfData;

namespace ComEngineers;

public partial class DataInputForm : Form
{
    public readonly EntityFrameContext Context;
    private int _selectedId;
    private Session? _selectedSession;

    public DataInputForm(EntityFrameContext context)
    {
        InitializeComponent();
        Context = context;
        TypesOfData.AddData(Context);

        foreach (var item in SessionData.GetSessionIds(Context))
        {
            sessionListBox.Items.Add(item);
        }
    } 


    private void Upload_Button_Click(object sender, EventArgs e)
    {
        var dialog = new OpenFileDialog();
        dialog.Filter = "CSV Files | *.csv"; // file types, that will be allowed to upload
        dialog.Multiselect = false;
        if (dialog.ShowDialog() != DialogResult.OK) return;
        var path = dialog.FileName;
        try
        {
            using var reader = new StreamReader(new FileStream(path, FileMode.Open), new UTF8Encoding());
            var content = reader.ReadToEnd();
            ParseData(content);
        }
        catch (IOException)
        {
            const string confirmationCaption = "Failed to Read File";
            const string confirmationMessage = "Please ensure selected file is not open in another program";

            MessageBox.Show(confirmationMessage, confirmationCaption, MessageBoxButtons.OK);
        }
    }

    private void label1_Click(object sender, EventArgs e)
    {
    }

    private void ParseData(string content)
    {
        var sessionEntries = new List<Session>();
       

        var data = DataInput.ProcessData(content, Context);

        const string confirmationCaption = "Data Storage Confirmation";
        const string confirmationMessage = "Upload Complete!\nDo you wish you store this data?";

        var confirmResult = MessageBox.Show(confirmationMessage, confirmationCaption, MessageBoxButtons.YesNo);

        if (confirmResult != DialogResult.Yes) return;

        DataInput.AddProcessedData(Context, data);
        var newSession = SessionData.GetLatestSession(Context);
        sessionListBox.Items.Add(newSession.Id);
    }

    private void RequestLatest_Click(object sender, EventArgs e)
    {
        var session = SessionData.GetLatestSession(Context);
        SessionId_Label.Text = session.Id.ToString();
    }

    private void SessionListBoxSelectedIndexChanged(object sender, EventArgs e)
    {
        _selectedId = int.Parse(sessionListBox.SelectedItem?.ToString() ?? "-1");
    }

    private void LoadSessionButton_Click(object sender, EventArgs e)
    {
        if (_selectedId == 0)
            // Don't accept just pressing button without input
            return;

        _selectedSession = SessionData.GetSessionById(Context, _selectedId);
        SessionId_Label.Text = _selectedSession?.Id.ToString();
    }

    private void DisplayDataButton_Click(object sender, EventArgs e)
    {
        (new DataDisplayForm(this, _selectedSession)).Show(); this.Hide();
    }

    private void DataInputForm_Load(object sender, EventArgs e)
    {
    }
}