using PizzaApi.Models;

namespace PizzaApi.Data
{
    public static class PizzaRepository
    {
        private static readonly List<Pizza> _pizzas = new()
        {
            new Pizza { Id = 1, Name = "Margherita", IsGlutenFree = false },
            new Pizza { Id = 2, Name = "Pepperoni",  IsGlutenFree = false },
            new Pizza { Id = 3, Name = "Veggie",     IsGlutenFree = true  }
        };
        private static int _nextId = 4;

        public static IEnumerable<Pizza> GetAll() => _pizzas;

        public static Pizza? Get(int id) => _pizzas.FirstOrDefault(p => p.Id == id);

        public static Pizza Add(Pizza pizza)
        {
            pizza.Id = _nextId++;
            _pizzas.Add(pizza);
            return pizza;
        }

        public static bool Update(int id, Pizza pizza)
        {
            var existing = Get(id);
            if (existing is null) return false;

            existing.Name = pizza.Name;
            existing.IsGlutenFree = pizza.IsGlutenFree;
            return true;
        }

        public static bool Delete(int id)
        {
            var existing = Get(id);
            if (existing is null) return false;
            _pizzas.Remove(existing);
            return true;
        }
    }
}
