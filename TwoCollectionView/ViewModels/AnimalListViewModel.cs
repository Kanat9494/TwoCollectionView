using MvvmHelpers;
using ObservableObject = Microsoft.Toolkit.Mvvm.ComponentModel.ObservableObject;

namespace TwoCollectionView.ViewModels;

public partial class AnimalListViewModel : ObservableObject
{
    private readonly AnimalService _animalService;
    public ObservableRangeCollection<EntryDetails> AnimalList { get; set; } = new ObservableRangeCollection<EntryDetails>();
    public ObservableRangeCollection<ObservableRangeCollection<EntryDetails>> AnimalList2 { get; set; } = new ObservableRangeCollection<ObservableRangeCollection<EntryDetails>>();
    ParentEntryDetails test1 = new ParentEntryDetails();
    ParentEntryDetails test2 = new ParentEntryDetails();

    public ObservableRangeCollection<ParentEntryDetails> ParentTestList { get; set; } = new ObservableRangeCollection<ParentEntryDetails>();

    [ObservableProperty]
    private ParentEntryDetails _parentEntryDetails;
    private List<EntryDetails> _allAnimals;
    private int _pageSize = 20;
    [ObservableProperty]
    private bool _isBusy;
    [ObservableProperty]
    private bool _isLoading;

    public AnimalListViewModel() { }
    public AnimalListViewModel(AnimalService animalService)
    {
        _animalService = animalService;

        ParentEntryDetails = new ParentEntryDetails();

        //AnimalList2.Add(AnimalList);
        //AnimalList2.Add(AnimalList);



        GetAnimalList();
    }
    private void GetAnimalList()
    {
        AnimalList.Clear();
        //AnimalList2.Clear();
        ParentTestList.Clear();


        IsBusy = true;
        Task.Run(async () =>
        {
            _allAnimals = await _animalService.GetAnimalList();

            App.Current.Dispatcher.Dispatch(() =>
            {
                AnimalList.ReplaceRange(_allAnimals.Take(_pageSize).ToList());

                ParentTestList.Add(new ParentEntryDetails()
                {
                    ParentList = _allAnimals.Take(_pageSize).ToList(),
                    ParentList2 = _allAnimals.Take(_pageSize).ToList(),
                    Test = "Test text1"
                });


                //foreach (var item in _allAnimals)
                //{
                //    ParentTestList[0].Add(item);
                //    ParentTestList[1].ParentList.Add(item);
                //}

                //ParentTestList[0].ParentList.ReplaceRange(_allAnimals.Take(_pageSize).ToList());
                //ParentTestList[1].ParentList.ReplaceRange(_allAnimals.Take(_pageSize).ToList());



                //ParentEntryDetails.ParentList.ReplaceRange(_allAnimals.Take(_pageSize).ToList());

                //AnimalList2[0].ReplaceRange(_allAnimals.Take(_pageSize).ToList());
                //AnimalList2[1].ReplaceRange(_allAnimals.Take(_pageSize).ToList());


                //AnimalList.AddRange(AnimalList);
                //AnimalList.AddRange(AnimalList);



                IsBusy = false;
            });
        });
    }

    [ICommand]
    public async Task LoadMoreData()
    {
        if (IsLoading) return;

        if (_allAnimals?.Count > 0)
        {
            IsLoading = true;
            await Task.Delay(2000);
            //AnimalList.AddRange(_allAnimals.Skip(AnimalList.Count).Take(_pageSize).ToList());
            //AnimalList2[1].AddRange(_allAnimals.Skip(AnimalList2[1].Count)
            //    .Take(_pageSize).ToList());

            ParentTestList[0].ParentList2.AddRange(_allAnimals
                .Skip(ParentTestList[0].ParentList2.Count)
                .Take(_pageSize).ToList());

            IsLoading = false;
        }
    }
}
