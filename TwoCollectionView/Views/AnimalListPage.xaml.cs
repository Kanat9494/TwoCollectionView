namespace TwoCollectionView.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class AnimalListPage : ContentPage
{
	public AnimalListPage(AnimalListViewModel viewModel)
	{
		InitializeComponent();

        this.BindingContext = viewModel;
    }
}