using DropsLibrary;
using DropsUI.Controls;
using System.Net.Sockets;

// TODO FlowTables in flowtables in flowtables
// Design rounded border flowtable?
namespace DropsUI;

public partial class Form1 : GradientForm
{
    /// <summary>
    /// Test data to build app until database is linked
    /// </summary>
    public SampleData sample = new();

    public List<Bucket> utilities = new();
    public List<Bucket> subscriptions = new();

    public Form1()
    {
        InitializeComponent();
        PopulateBucketLists();
        DisplayTotals();
        DisplayCategory(pnlUtilities, utilities);
        DisplayCategory(pnlSubscriptions, subscriptions);
    }

    private void PopulateBucketLists()
    {
        foreach (Bucket bucket in sample.Buckets)
        {
            if (bucket.Category == Category.Utilities)
            {
                utilities.Add(bucket);
            }
            else if (bucket.Category == Category.Subscription)
            {
                subscriptions.Add(bucket);
            }
        }
    }

    private void DisplayTotals()
    {
        lblUtiltiesBudget.Text = GetTotalBudget(utilities).ToString("C");
        lblUtilitiesAvailable.Text = GetTotalAvailable(utilities).ToString("C");
        lblSubscriptionsBudget.Text = GetTotalBudget(subscriptions).ToString("C");
        lblSubscriptionsAvailable.Text = GetTotalAvailable(subscriptions).ToString("C");
    }

    private void Form1_Resize(object sender, EventArgs e)
    {
        // Redraw on resizing to prevent black line artifacts and color
        // gradient errors
        Invalidate();
    }

    private void TglUtilities_CheckedChanged(object sender, EventArgs e)
    {
        if (!tglUtilities.Checked)
        {
            pnlUtilities.Controls.Clear();
        }
        else
        {
            DisplayCategory(pnlUtilities, utilities);
        }
    }

    private void TglSubscriptions_CheckedChanged(object sender, EventArgs e)
    {
        if (!tglSubscriptions.Checked)
        {
            pnlSubscriptions.Controls.Clear();
        }
        else
        {
            DisplayCategory(pnlSubscriptions, subscriptions);
        }
    }

    private static void DisplayCategory(TableLayoutPanel panel, List<Bucket> buckets)
    {
        panel.SuspendLayout();
        panel.Controls.Clear();

        // Expand the display to show individual buckets in the category.
        foreach (var bucket in buckets)
        {
            Label name = CreateLblName(bucket);
            Label budget = CreateLblBudget(bucket);
            RoundButton available = CreateBtnAvailable(bucket);

            panel.Controls.Add(name, 1, buckets.IndexOf(bucket));
            panel.Controls.Add(budget, 2, buckets.IndexOf(bucket));
            panel.Controls.Add(available, 3, buckets.IndexOf(bucket));

            // Signal to user status of the available funding.
            SignalButtons(panel, bucket);
        }
        panel.ResumeLayout();
    }

    private static Label CreateLblName(Bucket bucket)
    {
        return new()
        {
            Text = bucket.Name,
            AutoSize = true,
            Anchor = AnchorStyles.Left
        };
    }

    private static Label CreateLblBudget(Bucket bucket)
    {
        return new()
        {
            Text = bucket.Budget.ToString("C"),
            Anchor = AnchorStyles.Right,
            AutoSize = true
        };
    }

    private static RoundButton CreateBtnAvailable(Bucket bucket)
    {
        return new()
        {
            Name = "btn" + bucket.Name,
            Text = bucket.WaterLevel.ToString("C"),
            TextAlign = ContentAlignment.TopRight,
            Anchor = AnchorStyles.Right,
            Size = new(120, 30),
        };
    }

    private static void SignalButtons(TableLayoutPanel panel, Bucket bucket)
    {
        Button? button = null;
        if (panel.Controls.ContainsKey("btn" + bucket.Name))
        {
            button = panel.Controls["btn" + bucket.Name] as Button;
        }

        if (button != null)
        {
            if (bucket.WaterLevel < 0)
            {
                button.BackColor = Color.Red;
            }
            else if (bucket.WaterLevel == 0m)
            {
                button.BackColor = Color.Yellow;
                button.ForeColor = Color.Black;
            }
            else if (bucket.WaterLevel < bucket.Budget)
            {
                button.BackColor = Color.Blue;
            }
            else if (bucket.WaterLevel >= bucket.Budget)
            {
                button.BackColor = Color.Green;
            }
        }
    }

    // Acounting Methods
    private static decimal GetTotalBudget(List<Bucket> buckets)
    {
        decimal total = 0;
        foreach (var bucket in buckets)
        {
            total += bucket.Budget;
        }
        return total;
    }

    private static decimal GetTotalAvailable(List<Bucket> buckets)
    {
        decimal total = 0;
        foreach (var bucket in buckets)
        {
            total += bucket.WaterLevel;
        }
        return total;
    }
}