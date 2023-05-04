using PizzaCuDeToateAPI.DataContexts;
using PizzaCuDeToateAPI.Models;

namespace PizzaCuDeToateAPI;

public static class DataSeeder
{
    public static void Seed(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        context.Database.EnsureCreated();
        AddItems(context);
    }

    private static void AddItems(ApplicationContext context)
    {
        //Categories

        var categoryMeat =
            new Category(0, "Meat",
                "Animal flesh that is eaten as food", "");
        var categoryCheese =
            new Category(1, "Cheese",
                "Cheese is a dairy product produced in wide ranges of flavors, textures, and forms by coagulation of the milk protein casein",
                "");
        var categorySauce =
            new Category(2, "Sauce",
                "Liquid or semiliquid mixture that is added to a food as it cooks or that is served with it", "");
        var categoryVegetables =
            new Category(3, "Vegetables",
                "The edible portion of a plant", "");
        var categoryPastry =
            new Category(4, "Pastry",
                "A type of dough made with flour, water and shortening", "");
        var categoryPizza =
            new Category(5, "Pizza",
                "It must have dough, tomato sauce and cheese", "");
        var categoryShawarma =
            new Category(6, "Shawarma",
                "Is a popular Middle Eastern dish that originated in the Ottoman Empire, consisting of meat cut into thin slices",
                "");

        //Food Items

        var foodItemPizzaCalifornia =
            new FoodItem(0, "Californa", categoryPizza, "", 25, new List<string>(), "");
        var foodItemPizzaVeggieMozzarella =
            new FoodItem(1, "Veggie & Mozzarella", categoryPizza, "", 25, new List<string>(), "");
        var foodItemPizzaSuprema =
            new FoodItem(2, "Suprema", categoryPizza, "", 25, new List<string>(), "");
        var foodItemPizzaPepperoni =
            new FoodItem(3, "Pepperoni", categoryPizza, "", 25, new List<string>(), "");
        var foodItemPizzaAmericanSpicy =
            new FoodItem(4, "American Spicy", categoryPizza, "", 25, new List<string>(), "");
        var foodItemPizzaChickenCorn =
            new FoodItem(5, "Chicken & Corn", categoryPizza, "", 25, new List<string>(), "");
        var foodItemPizzaPepperoniFeta =
            new FoodItem(6, "Pepperoni & Feta", categoryPizza, "", 25, new List<string>(), "");
        var foodItemPizzaQuattroFormaggi =
            new FoodItem(7, "Quattro Formaggi", categoryPizza, "", 25, new List<string>(), "");
        var foodItemPizzaMeatLovers =
            new FoodItem(8, "Meat Lovers", categoryPizza, "", 25, new List<string>(), "");
        var foodItemShawarmaComfortShaorma =
            new FoodItem(9, "Comfort Shawarma", categoryShawarma, "", 25, new List<string>(), "");
        var foodItemShawarmaShaormaCuCeva =
            new FoodItem(10, "Shawarma cu ceva", categoryPizza, "", 25, new List<string>(), "");
        var foodItemShawarmaShaormaSaracului =
            new FoodItem(11, "Shawarma Saracului", categoryShawarma, "", 25, new List<string>(), "");

        //Stock Items

        var stockItemMozzarella =
            new StockItem(0, "Mozzarella", categoryCheese, true, "50g", 5, 10, "");
        var stockItemCheddar =
            new StockItem(1, "Cheddar", categoryCheese, true, "50g", 5, 10, "");
        var stockItemEmmentaler =
            new StockItem(2, "Emmentaler", categoryCheese, true, "50g", 5, 10, "");
        var stockItemGorgonzola =
            new StockItem(3, "Gorgonzola", categoryCheese, true, "50g", 5, 10, "");
        var stockItemFetaCheese =
            new StockItem(4, "Feta Cheese", categoryCheese, true, "50g", 5, 10, "");
        var stockItemTomatoSauce =
            new StockItem(5, "Tomato Sauce", categorySauce, true, "50g", 5, 10, "");
        var stockItemMayonnaise =
            new StockItem(6, "Mozzarella", categorySauce, true, "50g", 5, 10, "");
        var stockItemKetchup =
            new StockItem(7, "ketchup", categorySauce, true, "50g", 5, 10, "");
        var stockItemSpicyMayonnaise =
            new StockItem(8, "Spicy Mayonnaise", categorySauce, true, "50g", 5, 10, "");
        var stockItemSamuraiSauce =
            new StockItem(9, "Samurai Sauce", categorySauce, true, "50g", 5, 10, "");
        var stockItemChicken =
            new StockItem(10, "Chicken", categoryMeat, true, "50g", 5, 10, "");
        var stockItemBeef =
            new StockItem(11, "Beef", categoryMeat, true, "50g", 5, 10, "");
        var stockItemPork =
            new StockItem(12, "Pork", categoryMeat, true, "50g", 5, 10, "");
        var stockItemPepperoni =
            new StockItem(13, "Pepperoni", categoryMeat, true, "50g", 5, 10, "");
        var stockItemBacon =
            new StockItem(14, "Bacon", categoryMeat, true, "50g", 5, 10, "");
        var stockItemSmallPita =
            new StockItem(15, "Small Pita", categoryPastry, true, "50g", 5, 10, "");
        var stockItemBigPita =
            new StockItem(16, "Big pita", categoryPastry, true, "50g", 5, 10, "");
        var stockItemPepper =
            new StockItem(17, "Pepper", categoryVegetables, true, "50g", 5, 10, "");
        var stockItemOnion =
            new StockItem(18, "Onion", categoryVegetables, true, "50g", 5, 10, "");
        var stockItemTomatoes =
            new StockItem(19, "Tomatoes", categoryVegetables, true, "50g", 5, 10, "");
        var stockItemCorn =
            new StockItem(20, "Corn", categoryVegetables, true, "50g", 5, 10, "");
        var stockItemOlives =
            new StockItem(21, "Olives", categoryVegetables, true, "50g", 5, 10, "");
        var stockItemMushrooms =
            new StockItem(22, "Mushrooms", categoryVegetables, true, "50g", 5, 10, "");
        var stockItemJalapenos =
            new StockItem(23, "Jalapeños", categoryVegetables, true, "50g", 5, 10, "");
        var stockItemWhiteCabbage =
            new StockItem(24, "White Cabbage", categoryVegetables, true, "50g", 5, 10, "");
        var stockItemRedCabbage =
            new StockItem(25, "Red Cabbage", categoryVegetables, true, "50g", 5, 10, "");
        var stockItemPotatoes =
            new StockItem(26, "Potatoes", categoryVegetables, true, "50g", 5, 10, "");
        var stockItemPickles =
            new StockItem(27, "Pickles", categoryVegetables, true, "50g", 5, 10, "");
        var stockItemSalad =
            new StockItem(28, "Salad", categoryVegetables, true, "50g", 5, 10, "");

        //Ingredient List

        List<StockItem> pizzaCaliforniaIngredients =
            new()
            {
                stockItemTomatoSauce, stockItemMozzarella, stockItemChicken,
                stockItemCorn, stockItemPepper
            };
        List<StockItem> pizzaVeggieMozzarellaIngredients =
            new()
            {
                stockItemTomatoSauce, stockItemMozzarella, stockItemMushrooms,
                stockItemCorn, stockItemPepper, stockItemOlives, stockItemTomatoes
            };
        List<StockItem> pizzaSupremaIngredients =
            new()
            {
                stockItemTomatoSauce, stockItemMozzarella, stockItemPepperoni,
                stockItemBeef, stockItemPepper, stockItemOnion, stockItemMushrooms
            };
        List<StockItem> pizzaPepperoniIngredients =
            new() { stockItemTomatoSauce, stockItemMozzarella, stockItemPepperoni };
        List<StockItem> pizzaAmericanSpicyIngredients =
            new()
            {
                stockItemTomatoSauce, stockItemMozzarella, stockItemPepperoni,
                stockItemTomatoes, stockItemPepper, stockItemOnion, stockItemJalapenos, stockItemSamuraiSauce
            };
        List<StockItem> pizzaChickenCornIngredients =
            new()
            {
                stockItemTomatoSauce, stockItemMozzarella, stockItemChicken,
                stockItemCorn, stockItemPepper, stockItemMushrooms, stockItemOlives, stockItemTomatoes
            };
        List<StockItem> pizzaPepperoniFetaIngredients =
            new()
            {
                stockItemTomatoSauce, stockItemMozzarella, stockItemFetaCheese,
                stockItemPepperoni, stockItemPepper, stockItemOnion
            };
        List<StockItem> pizzaQuattroFormaggiIngredients =
            new()
            {
                stockItemTomatoSauce, stockItemMozzarella, stockItemCheddar,
                stockItemGorgonzola, stockItemEmmentaler
            };
        List<StockItem> pizzaMeatLoversIngredients =
            new()
            {
                stockItemTomatoSauce, stockItemMozzarella, stockItemChicken,
                stockItemBeef, stockItemBacon, stockItemPepperoni
            };
        List<StockItem> shawarmaComfortShawarmaIngredients =
            new()
            {
                stockItemSmallPita, stockItemChicken, stockItemPotatoes,
                stockItemPickles, stockItemWhiteCabbage, stockItemMayonnaise, stockItemKetchup
            };
        List<StockItem> shawarmaShawarmaCuCevaIngredients =
            new()
            {
                stockItemBigPita, stockItemBeef, stockItemPotatoes,
                stockItemTomatoes, stockItemRedCabbage, stockItemWhiteCabbage,
                stockItemMayonnaise, stockItemSpicyMayonnaise
            };
        List<StockItem> shawarmaShawarmaSaraculuiIngredients =
            new()
            {
                stockItemBigPita, stockItemBeef, stockItemTomatoes,
                stockItemSalad, stockItemPickles, stockItemKetchup
            };

        //food item list corresponding to each ingredient

        List<FoodItem> mozzarellaContainingFoodItems =
            new()
            {
                foodItemPizzaCalifornia, foodItemPizzaVeggieMozzarella, foodItemPizzaSuprema, foodItemPizzaPepperoni,
                foodItemPizzaAmericanSpicy, foodItemPizzaChickenCorn, foodItemPizzaPepperoniFeta,
                foodItemPizzaQuattroFormaggi, foodItemPizzaMeatLovers
            };
        List<FoodItem> cheddarContainingFoodItems =
            new() { foodItemPizzaQuattroFormaggi };
        List<FoodItem> emmentalerContainingFoodItems =
            new() { foodItemPizzaQuattroFormaggi };
        List<FoodItem> gorgonzolaContainingFoodItems =
            new() { foodItemPizzaQuattroFormaggi };
        List<FoodItem> fetaCheeseContainingFoodItems =
            new() { foodItemPizzaPepperoniFeta };
        List<FoodItem> tomatoSauceContainingFoodItems =
            new()
            {
                foodItemPizzaCalifornia,
                foodItemPizzaVeggieMozzarella, foodItemPizzaSuprema, foodItemPizzaPepperoni, foodItemPizzaAmericanSpicy,
                foodItemPizzaChickenCorn, foodItemPizzaPepperoniFeta, foodItemPizzaQuattroFormaggi,
                foodItemPizzaMeatLovers
            };
        List<FoodItem> mayonnaiseContainingFoodItems =
            new() { foodItemShawarmaShaormaCuCeva, foodItemShawarmaComfortShaorma };
        List<FoodItem> ketchupContainingFoodItems =
            new() { foodItemShawarmaComfortShaorma, foodItemShawarmaShaormaSaracului };
        List<FoodItem> spicyMayonnaiseContainingFoodItems =
            new() { foodItemShawarmaShaormaCuCeva };
        List<FoodItem> samuraiSauceContainingFoodItems =
            new() { foodItemPizzaAmericanSpicy };
        List<FoodItem> chickenContainingFoodItems =
            new()
            {
                foodItemShawarmaComfortShaorma, foodItemPizzaChickenCorn, foodItemPizzaCalifornia,
                foodItemPizzaMeatLovers
            };
        List<FoodItem> beefContainingFoodItems =
            new()
            {
                foodItemShawarmaShaormaSaracului, foodItemShawarmaShaormaCuCeva, foodItemPizzaSuprema,
                foodItemPizzaMeatLovers
            };
        List<FoodItem> pepperoniContainingFoodItems =
            new()
            {
                foodItemPizzaPepperoniFeta, foodItemPizzaPepperoni, foodItemPizzaSuprema, foodItemPizzaMeatLovers,
                foodItemPizzaAmericanSpicy
            };
        List<FoodItem> baconContainingFoodItems =
            new() { foodItemPizzaMeatLovers };
        List<FoodItem> smallPitaContainingFoodItems =
            new() { foodItemShawarmaComfortShaorma };
        List<FoodItem> bigPitaContainingFoodItems =
            new() { foodItemShawarmaShaormaCuCeva, foodItemShawarmaShaormaSaracului };
        List<FoodItem> pepperContainingFoodItems =
            new()
            {
                foodItemPizzaCalifornia, foodItemPizzaVeggieMozzarella, foodItemPizzaSuprema,
                foodItemPizzaAmericanSpicy,
                foodItemPizzaChickenCorn, foodItemPizzaPepperoniFeta
            };
        List<FoodItem> onionContainingFoodItems =
            new() { foodItemPizzaSuprema, foodItemPizzaAmericanSpicy, foodItemPizzaPepperoniFeta };
        List<FoodItem> tomatoesContainingFoodItems =
            new()
            {
                foodItemShawarmaShaormaCuCeva, foodItemShawarmaShaormaSaracului, foodItemPizzaVeggieMozzarella,
                foodItemPizzaAmericanSpicy, foodItemPizzaChickenCorn
            };
        List<FoodItem> cornContainingFoodItems =
            new() { foodItemPizzaCalifornia, foodItemPizzaVeggieMozzarella, foodItemPizzaChickenCorn };
        List<FoodItem> olivesContainingFoodItems =
            new() { foodItemPizzaVeggieMozzarella, foodItemPizzaChickenCorn };
        List<FoodItem> mushroomsContainingFoodItems =
            new() { foodItemPizzaVeggieMozzarella, foodItemPizzaSuprema, foodItemPizzaChickenCorn };
        List<FoodItem> jalapenosContainingFoodItems =
            new() { foodItemPizzaAmericanSpicy };
        List<FoodItem> whiteCabbageContainingFoodItems =
            new() { foodItemShawarmaShaormaCuCeva, foodItemShawarmaComfortShaorma };
        List<FoodItem> redCabbageContainingFoodItems =
            new() { foodItemShawarmaShaormaCuCeva };
        List<FoodItem> potatoesContainingFoodItems =
            new() { foodItemShawarmaShaormaCuCeva, foodItemShawarmaComfortShaorma };
        List<FoodItem> picklesContainingFoodItems =
            new() { foodItemShawarmaShaormaSaracului, foodItemShawarmaComfortShaorma };
        List<FoodItem> saladContainingFoodItems =
            new() { foodItemShawarmaShaormaSaracului };

        //adding ingredient lists to food items

        foodItemPizzaCalifornia.Ingredients = pizzaCaliforniaIngredients;
        foodItemPizzaVeggieMozzarella.Ingredients = pizzaVeggieMozzarellaIngredients;
        foodItemPizzaSuprema.Ingredients = pizzaSupremaIngredients;
        foodItemPizzaPepperoni.Ingredients = pizzaPepperoniIngredients;
        foodItemPizzaAmericanSpicy.Ingredients = pizzaAmericanSpicyIngredients;
        foodItemPizzaChickenCorn.Ingredients = pizzaChickenCornIngredients;
        foodItemPizzaPepperoniFeta.Ingredients = pizzaPepperoniFetaIngredients;
        foodItemPizzaQuattroFormaggi.Ingredients = pizzaQuattroFormaggiIngredients;
        foodItemPizzaMeatLovers.Ingredients = pizzaMeatLoversIngredients;
        foodItemShawarmaComfortShaorma.Ingredients = shawarmaComfortShawarmaIngredients;
        foodItemShawarmaShaormaCuCeva.Ingredients = shawarmaShawarmaCuCevaIngredients;
        foodItemShawarmaShaormaSaracului.Ingredients = shawarmaShawarmaSaraculuiIngredients;

        //adding food items lists to ingredients
        stockItemMozzarella.Meals = mozzarellaContainingFoodItems;
        stockItemCheddar.Meals = cheddarContainingFoodItems;
        stockItemEmmentaler.Meals = emmentalerContainingFoodItems;
        stockItemGorgonzola.Meals = gorgonzolaContainingFoodItems;
        stockItemFetaCheese.Meals = fetaCheeseContainingFoodItems;
        stockItemTomatoSauce.Meals = tomatoSauceContainingFoodItems;
        stockItemMayonnaise.Meals = mayonnaiseContainingFoodItems;
        stockItemSpicyMayonnaise.Meals = spicyMayonnaiseContainingFoodItems;
        stockItemKetchup.Meals = ketchupContainingFoodItems;
        stockItemSamuraiSauce.Meals = samuraiSauceContainingFoodItems;
        stockItemChicken.Meals = chickenContainingFoodItems;
        stockItemBeef.Meals = beefContainingFoodItems;
        stockItemPepperoni.Meals = pepperoniContainingFoodItems;
        stockItemBacon.Meals = baconContainingFoodItems;
        stockItemSmallPita.Meals = smallPitaContainingFoodItems;
        stockItemBigPita.Meals = bigPitaContainingFoodItems;
        stockItemPepper.Meals = pepperContainingFoodItems;
        stockItemOnion.Meals = onionContainingFoodItems;
        stockItemTomatoes.Meals = tomatoesContainingFoodItems;
        stockItemCorn.Meals = cornContainingFoodItems;
        stockItemOlives.Meals = olivesContainingFoodItems;
        stockItemMushrooms.Meals = mushroomsContainingFoodItems;
        stockItemJalapenos.Meals = jalapenosContainingFoodItems;
        stockItemWhiteCabbage.Meals = whiteCabbageContainingFoodItems;
        stockItemRedCabbage.Meals = redCabbageContainingFoodItems;
        stockItemPotatoes.Meals = potatoesContainingFoodItems;
        stockItemPickles.Meals = picklesContainingFoodItems;
        stockItemSalad.Meals = saladContainingFoodItems;

        // database


        //adding categories to database

        var cateogry = context.Categories.FirstOrDefault();
        if (cateogry == null)
        {
            context.Categories.Add(categoryMeat);
            context.Categories.Add(categoryCheese);
            context.Categories.Add(categorySauce);
            context.Categories.Add(categoryVegetables);
            context.Categories.Add(categoryPastry);
            context.Categories.Add(categoryPizza);
            context.Categories.Add(categoryShawarma);
        }
        //adding food items to database

        var fooditem = context.FoodItems.FirstOrDefault();
        if (fooditem == null)
        {
            context.FoodItems.Add(foodItemPizzaCalifornia);
            context.FoodItems.Add(foodItemPizzaVeggieMozzarella);
            context.FoodItems.Add(foodItemPizzaSuprema);
            context.FoodItems.Add(foodItemPizzaPepperoni);
            context.FoodItems.Add(foodItemPizzaAmericanSpicy);
            context.FoodItems.Add(foodItemPizzaChickenCorn);
            context.FoodItems.Add(foodItemPizzaPepperoniFeta);
            context.FoodItems.Add(foodItemPizzaQuattroFormaggi);
            context.FoodItems.Add(foodItemPizzaMeatLovers);
            context.FoodItems.Add(foodItemShawarmaComfortShaorma);
            context.FoodItems.Add(foodItemShawarmaShaormaCuCeva);
            context.FoodItems.Add(foodItemShawarmaShaormaSaracului);
        }   
        //adding stock items to database

        var stockItem = context.FoodItems.FirstOrDefault();
        if (stockItem == null)
        {
            context.StockItems.Add(stockItemMozzarella);
            context.StockItems.Add(stockItemCheddar);
            context.StockItems.Add(stockItemEmmentaler);
            context.StockItems.Add(stockItemGorgonzola);
            context.StockItems.Add(stockItemFetaCheese);
            context.StockItems.Add(stockItemTomatoSauce);
            context.StockItems.Add(stockItemMayonnaise);
            context.StockItems.Add(stockItemKetchup);
            context.StockItems.Add(stockItemSpicyMayonnaise);
            context.StockItems.Add(stockItemSamuraiSauce);
            context.StockItems.Add(stockItemChicken);
            context.StockItems.Add(stockItemBeef);
            context.StockItems.Add(stockItemPork);
            context.StockItems.Add(stockItemPepperoni);
            context.StockItems.Add(stockItemBacon);
            context.StockItems.Add(stockItemSmallPita);
            context.StockItems.Add(stockItemBigPita);
            context.StockItems.Add(stockItemPepper);
            context.StockItems.Add(stockItemOnion);
            context.StockItems.Add(stockItemTomatoes);
            context.StockItems.Add(stockItemCorn);
            context.StockItems.Add(stockItemOlives);
            context.StockItems.Add(stockItemMushrooms);
            context.StockItems.Add(stockItemJalapenos);
            context.StockItems.Add(stockItemWhiteCabbage);
            context.StockItems.Add(stockItemRedCabbage);
            context.StockItems.Add(stockItemPotatoes);
            context.StockItems.Add(stockItemPickles);
            context.StockItems.Add(stockItemSalad);
        }

        context.SaveChanges();
    }
}