using DropsLibrary;
using DropsUI.Controls;
using System.Net.Sockets;

// TODO FlowTables in flowtables in flowtables
// Design rounded border flowtable?
namespace DropsUI;

public partial class Form1 : GradientForm
{
    public List<Bucket> utilities = new();
    public List<Bucket> subscriptions = new();

    // TEST CODE: Buckets should be generated from DB data
    private Bucket electricBill = new()
    {
        Name = "Electric Bill",
        Category = Category.Utilities,
        Budget = 3_500M,
        Drops = 150M
    };

    private Bucket internet = new()
    {
        Name = "Internet",
        Category = Category.Utilities,
        Budget = 115.27M,
        Drops = 0M
    };

    private Bucket netflix = new()
    {
        Name = "Netflix",
        Category = Category.Subscription,
        Budget = 15M,
        Drops = 10M
    };

    // END TEST CODE

    public Form1()
    {
        InitializeComponent();

        // TEST CODE Add buckets from test code above
        utilities.Add(electricBill);
        utilities.Add(internet);
        subscriptions.Add(netflix);
        // END TEST CODE

        PopulateTotals();
        PopulateCategory(pnlUtilities, utilities);
        PopulateCategory(pnlSubscriptions, subscriptions);
    }

    private void PopulateTotals()
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
            PopulateCategory(pnlUtilities, utilities);
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
            PopulateCategory(pnlSubscriptions, subscriptions);
        }
    }

    private static void PopulateCategory(TableLayoutPanel panel, List<Bucket> buckets)
    {
        panel.SuspendLayout();
        panel.Controls.Clear();

        // Expand the display to show individual buckets in the category.
        foreach (var bucket in buckets)
        {
            Label name = new()
            {
                Text = bucket.Name,
                AutoSize = true,
                Anchor = AnchorStyles.Left
            };
            Label budget = new()
            {
                Text = bucket.Budget.ToString("C"),
                Anchor = AnchorStyles.Right,
                AutoSize = true
            };
            RoundButton available = new()
            {
                Text = bucket.Drops.ToString("C"),
                TextAlign = ContentAlignment.TopRight,
                Anchor = AnchorStyles.Right,
                Size = new(120, 30),
            };

            panel.Controls.Add(name, 1, buckets.IndexOf(bucket));
            panel.Controls.Add(budget, 2, buckets.IndexOf(bucket));
            panel.Controls.Add(available, 3, buckets.IndexOf(bucket));
        }
        panel.ResumeLayout();
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
            total += bucket.Drops;
        }
        return total;
    }
}