namespace ProdASP.Models
{
    public class SortViewModel
    {
        public SortState NameSort { get; }   
        public SortState Current { get; }    

        public SortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            Current = sortOrder;
        }
    }
}
