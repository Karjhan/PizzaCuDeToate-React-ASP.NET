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
            new Category( "Meat",
                "Animal flesh that is eaten as food", "");
        var categoryCheese =
            new Category( "Cheese",
                "Cheese is a dairy product produced in wide ranges of flavors, textures, and forms by coagulation of the milk protein casein",
                "");
        var categorySauce =
            new Category( "Sauce",
                "Liquid or semiliquid mixture that is added to a food as it cooks or that is served with it", "");
        var categoryVegetables =
            new Category( "Vegetables",
                "The edible portion of a plant", "");
        var categoryPastry =
            new Category( "Pastry",
                "A type of dough made with flour, water and shortening", "");
        var categoryPizza =
            new Category( "Pizza",
                "It must have dough, tomato sauce and cheese", "");
        var categoryShawarma =
            new Category( "Shawarma",
                "Is a popular Middle Eastern dish that originated in the Ottoman Empire, consisting of meat cut into thin slices",
                "");

        //Food Items

        var foodItemPizzaCalifornia =
            new FoodItem( "Californa", categoryPizza, "", 25, new List<string>(), "");
        var foodItemPizzaVeggieMozzarella =
            new FoodItem( "Veggie & Mozzarella", categoryPizza, "", 25, new List<string>(), "");
        var foodItemPizzaSuprema =
            new FoodItem( "Suprema", categoryPizza, "", 25, new List<string>(), "");
        var foodItemPizzaPepperoni =
            new FoodItem( "Pepperoni", categoryPizza, "", 25, new List<string>(), "");
        var foodItemPizzaAmericanSpicy =
            new FoodItem( "American Spicy", categoryPizza, "", 25, new List<string>(), "");
        var foodItemPizzaChickenCorn =
            new FoodItem( "Chicken & Corn", categoryPizza, "", 25, new List<string>(), "");
        var foodItemPizzaPepperoniFeta =
            new FoodItem( "Pepperoni & Feta", categoryPizza, "", 25, new List<string>(), "");
        var foodItemPizzaQuattroFormaggi =
            new FoodItem( "Quattro Formaggi", categoryPizza, "", 25, new List<string>(), "");
        var foodItemPizzaMeatLovers =
            new FoodItem( "Meat Lovers", categoryPizza, "", 25, new List<string>(), "");
        var foodItemShawarmaComfortShaorma =
            new FoodItem( "Comfort Shawarma", categoryShawarma, "", 25, new List<string>(), "");
        var foodItemShawarmaShaormaCuCeva =
            new FoodItem( "Shawarma cu ceva", categoryShawarma, "", 25, new List<string>(), "");
        var foodItemShawarmaShaormaSaracului =
            new FoodItem( "Shawarma Saracului", categoryShawarma, "", 25, new List<string>(), "");

        //Stock Items

        var stockItemMozzarella =
            new StockItem( "Mozzarella", categoryCheese, true, "50g", 5, 10, "");
        var stockItemCheddar =
            new StockItem( "Cheddar", categoryCheese, true, "50g", 5, 10, "");
        var stockItemEmmentaler =
            new StockItem( "Emmentaler", categoryCheese, true, "50g", 5, 10, "");
        var stockItemGorgonzola =
            new StockItem( "Gorgonzola", categoryCheese, true, "50g", 5, 10, "");
        var stockItemFetaCheese =
            new StockItem( "Feta Cheese", categoryCheese, true, "50g", 5, 10, "");
        var stockItemTomatoSauce =
            new StockItem( "Tomato Sauce", categorySauce, true, "50g", 5, 10, "");
        var stockItemMayonnaise =
            new StockItem( "Mozzarella", categorySauce, true, "50g", 5, 10, "");
        var stockItemKetchup =
            new StockItem( "ketchup", categorySauce, true, "50g", 5, 10, "");
        var stockItemSpicyMayonnaise =
            new StockItem( "Spicy Mayonnaise", categorySauce, true, "50g", 5, 10, "");
        var stockItemSamuraiSauce =
            new StockItem( "Samurai Sauce", categorySauce, true, "50g", 5, 10, "");
        var stockItemChicken =
            new StockItem( "Chicken", categoryMeat, true, "50g", 5, 10, "");
        var stockItemBeef =
            new StockItem( "Beef", categoryMeat, true, "50g", 5, 10, "");
        var stockItemPork =
            new StockItem( "Pork", categoryMeat, true, "50g", 5, 10, "");
        var stockItemPepperoni =
            new StockItem( "Pepperoni", categoryMeat, true, "50g", 5, 10, "");
        var stockItemBacon =
            new StockItem( "Bacon", categoryMeat, true, "50g", 5, 10, "");
        var stockItemSmallPita =
            new StockItem( "Small Pita", categoryPastry, true, "50g", 5, 10, "");
        var stockItemBigPita =
            new StockItem( "Big pita", categoryPastry, true, "50g", 5, 10, "");
        var stockItemPepper =
            new StockItem( "Pepper", categoryVegetables, true, "50g", 5, 10, "");
        var stockItemOnion =
            new StockItem( "Onion", categoryVegetables, true, "50g", 5, 10, "");
        var stockItemTomatoes =
            new StockItem( "Tomatoes", categoryVegetables, true, "50g", 5, 10, "");
        var stockItemCorn =
            new StockItem( "Corn", categoryVegetables, true, "50g", 5, 10, "");
        var stockItemOlives =
            new StockItem( "Olives", categoryVegetables, true, "50g", 5, 10, "");
        var stockItemMushrooms =
            new StockItem( "Mushrooms", categoryVegetables, true, "50g", 5, 10, "");
        var stockItemJalapenos =
            new StockItem( "Jalapeños", categoryVegetables, true, "50g", 5, 10, "");
        var stockItemWhiteCabbage =
            new StockItem( "White Cabbage", categoryVegetables, true, "50g", 5, 10, "");
        var stockItemRedCabbage =
            new StockItem( "Red Cabbage", categoryVegetables, true, "50g", 5, 10, "");
        var stockItemPotatoes =
            new StockItem( "Potatoes", categoryVegetables, true, "50g", 5, 10, "");
        var stockItemPickles =
            new StockItem( "Pickles", categoryVegetables, true, "50g", 5, 10, "");
        var stockItemSalad =
            new StockItem( "Salad", categoryVegetables, true, "50g", 5, 10, "");

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