namespace NudeSolution.Entities
{
    public class CategoryEntity
    {
        private List<ItemEntity> _items;
        private decimal _totalValue;
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ItemEntity> Items
        {
            get => _items; set
            {
                _items = value;
            }
        }
        public decimal TotalValue
        {
            get => _totalValue; set
            {
                if (_items != null && _items.Any())
                {

                    foreach (var item in _items)
                    {
                        _totalValue += item.Value;
                    }
                }
            }
        }
    }
}
