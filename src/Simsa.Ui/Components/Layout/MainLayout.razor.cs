namespace Simsa.Ui.Components.Layout;

public partial class MainLayout
{
    private bool drawerOpen;

    private void DrawerToggle()
    {
        this.drawerOpen = !this.drawerOpen;
    }
}