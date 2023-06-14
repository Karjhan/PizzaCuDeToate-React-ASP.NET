using System.Data.SqlClient;
using Npgsql;
using PizzaCuDeToateAPI.DataContexts;
using PizzaCuDeToateAPI.Models;

namespace PizzaCuDeToateAPI;

public static class DataSeeder
{
    public static void Seed(this IHost host, string connectionString)
    {
        using var scope = host.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        context.Database.EnsureCreated();
        ResetIdSequences(context, connectionString);
        AddItems(context);
    }

    private static void ResetIdSequences(ApplicationContext context, string connectionString)
    {
        var condition = context.Categories.FirstOrDefault() is null && context.StockItems.FirstOrDefault() is null && context.FoodItems.FirstOrDefault() is null;

        if (condition)
        {
            var connectionSource = NpgsqlDataSource.Create(connectionString);
            string[] queries = new[]
            {
                "ALTER SEQUENCE \"StockItems_Id_seq\" RESTART WITH 1", 
                "ALTER SEQUENCE \"FoodItems_Id_seq\" RESTART WITH 1",
                "ALTER SEQUENCE \"Categories_Id_seq\" RESTART WITH 1"
            };
            foreach (var query in queries)
            {
                using (var command = connectionSource.CreateCommand(query))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }

    private static void AddItems(ApplicationContext context)
    {
        //Categories

        var categoryMeat =
            new Category( "Meat",
                "Animal flesh that is eaten as food", "https://drive.google.com/uc?export=view&id=1IxxFAUdIcQ8KBV-tQgxJRIsJC_7SBwfi");
        var categoryCheese =
            new Category( "Cheese",
                "Cheese is a dairy product produced in wide ranges of flavors, textures, and forms by coagulation of the milk protein casein",
                "https://drive.google.com/uc?export=view&id=1UWqiRkILqTLCxLbA2pdbbmH8x5JdJzhk");
        var categorySauce =
            new Category( "Sauce",
                "Liquid or semiliquid mixture that is added to a food as it cooks or that is served with it", "https://drive.google.com/uc?export=view&id=1DEiXwKW3aHww1cg6u556cY21VmD7REyf");
        var categoryVegetables =
            new Category( "Vegetables",
                "The edible portion of a plant", "https://drive.google.com/uc?export=view&id=1hfzaAYA6suFaC1_sVDEzdXNHiXwBlk6w");
        var categoryPastry =
            new Category( "Pastry",
                "A type of dough made with flour, water and shortening", "https://drive.google.com/uc?export=view&id=1xYpFIxVa68ruHyECD8sCeQzqrahs6fG7");
        var categoryPizza =
            new Category( "Pizza",
                "It must have dough, tomato sauce and cheese", "https://drive.google.com/uc?export=view&id=1tjWiglPgpSPOfHlqP25IvMI13ovM8lgN");
        var categoryShawarma =
            new Category( "Shawarma",
                "Is a popular Middle Eastern dish that originated in the Ottoman Empire, consisting of meat cut into thin slices",
                "https://drive.google.com/uc?export=view&id=1-7adfBq7AlXH5aK67s3k82jS-ghy3DOh");
        var categoryDessert =
            new Category( "Dessert", "A dessert is a type of sweet food that is eaten after lunch or dinner, and sometimes after a light meal or snack", 
                "https://drive.google.com/uc?export=view&id=1RB-iEYBWXKJf3xzYR9hFqvLT6TQEyOWu");
        var categoryBeverage =
            new Category( "Beverage", "Any potable liquid, especially one other than water, as tea, coffee, beer, or milk.", "https://drive.google.com/uc?export=view&id=12umZs3jOWV6RSZ53f2k4EROu2Rx1zvkG");

        //Food Items

        var foodItemPizzaCalifornia =
            new FoodItem( "California", categoryPizza, "A light chicken pizza, inspired from the summers of California", 41.5, new List<string>(), "https://drive.google.com/uc?export=view&id=1nyiX5SVe8OT3X-FBdJYdIdVPKQyf2anq");
        var foodItemPizzaVeggieMozzarella =
            new FoodItem( "Veggie & Mozzarella", categoryPizza, "Vegetarian pizza is richer in nutrients and you need it", 39.5, new List<string>(), "https://drive.google.com/uc?export=view&id=1dvnTkYaq-E6I3SHlTMaaR6zKsp7aTyaQ");
        var foodItemPizzaSuprema =
            new FoodItem( "Suprema", categoryPizza, "Pork and beef combine excellently to offer the rich taste of meat", 49, new List<string>(), "https://drive.google.com/uc?export=view&id=1ro_T4BNAGpf9YuCYvg2l1MOcr_c_bV2s");
        var foodItemPizzaPepperoni =
            new FoodItem( "Pepperoni", categoryPizza, "Pepperoni includes one of the country's most beloved toppings: pepperoni salami", 44.5, new List<string>(), "https://drive.google.com/uc?export=view&id=1JihSU72Gc87hdc65jAzZJnFe8ZEobEYo");
        var foodItemPizzaAmericanSpicy =
            new FoodItem( "American Spicy", categoryPizza, "A spicy night with our fresh jalapenos and Samourai sauce", 44.5, new List<string>(), "https://drive.google.com/uc?export=view&id=19wgvl16GewjSLpqc0-M0GIt9PpV1acqa");
        var foodItemPizzaChickenCorn =
            new FoodItem( "Chicken & Corn", categoryPizza, "Chicken & Corn pizza is a delicious pizza that combines the flavors of tender chicken and sweet corn.", 44.5, new List<string>(), "https://drive.google.com/uc?export=view&id=1JVGLbt7l576M96TPee9R8f-agycWSnee");
        var foodItemPizzaPepperoniFeta =
            new FoodItem( "Pepperoni & Feta", categoryPizza, "A delicious combination of flavors that brings together the spiciness of pepperoni and the tanginess of feta cheese.", 43.5, new List<string>(), "https://drive.google.com/uc?export=view&id=1R2l08dy69345ABonwrEIsmu1ZxNrj8ol");
        var foodItemPizzaQuattroFormaggi =
            new FoodItem( "Quattro Formaggi", categoryPizza, "Mozzarella, Gorgonzola, Fontina, and Parmesan form a classic and beloved Italian resort in your mouth.", 49, new List<string>(), "https://drive.google.com/uc?export=view&id=1-wax65upHuM2MXXCT840LouF4WDNbBMS");
        var foodItemPizzaMeatLovers =
            new FoodItem( "Meat Lovers", categoryPizza, "It is known for its indulgent and savory flavor profile, making it a favorite choice for meat enthusiasts.", 49, new List<string>(), "https://drive.google.com/uc?export=view&id=1PPJ4-Oh6tBn4ABgnNMmJAW7b3WmntJ7w");
        var foodItemShawarmaComfortShaorma =
            new FoodItem( "Comfort Shawarma", categoryShawarma, "Chicken, pickles, potatoes and mayo in a simple wrap. No complicated stuff, we leave the complexities aside in this dish.", 38, new List<string>(), "https://drive.google.com/uc?export=view&id=1LNUjcnDOBzQmLLGKDY62SILvp-8aJ3m9");
        var foodItemShawarmaShaormaCuCeva =
            new FoodItem( "Shawarma With Something", categoryShawarma, "Enter in a realm of mysteries born from our shawarma taste innovations.", 41, new List<string>(), "https://drive.google.com/uc?export=view&id=1-zhzoSpqWx9XopKofr6Mm0AyhsOpMQwM");
        var foodItemShawarmaShaormaSaracului =
            new FoodItem( "Shawarma Saracului", categoryShawarma, "You don't have a lot of money on you, but want to experience the classic taste of a shawarma? Look no more!", 15, new List<string>(), "https://drive.google.com/uc?export=view&id=1mctK5by_Lak3YaVT_5nYdNmuwSvos_GQ");
        var foodItemShawarmaChicken = 
            new FoodItem("Chicken Shawarma", categoryShawarma, "Chicken shawarma is a popular Middle Eastern dish known for its flavorful and succulent grilled or roasted chicken.", 38.5, new List<string>(), "https://drive.google.com/uc?export=view&id=1BsaTFEvY5vWZoBn0JHNd1cLpRzT2RyB4");
        var foodItemShawarmaBeef = 
            new FoodItem("Beef Shawarma", categoryShawarma, "Beef shawarma is a popular Middle Eastern dish made with thinly sliced marinated beef that is typically slow-roasted on a vertical spit.", 48.5, new List<string>(), "https://drive.google.com/uc?export=view&id=1C8vPSjv_Hzu6ozNToLzGRNNe9ebvhEJk");
        var foodItemShawarmaMix =
            new FoodItem("Mix Shawarma", categoryShawarma, "A normal shawarma except the meat, which is a combination of beef and chicken: the best of both worlds.", 43.5, new List<string>(), "https://drive.google.com/uc?export=view&id=104-2ZSSfH3AfJPewTHPZmCB1X6V8oshw");

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
            new StockItem( "Ketchup", categorySauce, true, "50g", 5, 10, "");
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
                stockItemSmallPita, stockItemBeef, stockItemTomatoes,
                stockItemSalad, stockItemPickles, stockItemKetchup
            };
        List<StockItem> shawarmaShawarmaChickenIngredients =
            new()
            {
                stockItemBigPita, stockItemChicken, stockItemTomatoes, stockItemSalad, 
                stockItemPickles, stockItemKetchup, stockItemMayonnaise, stockItemPotatoes
            };
        List<StockItem> shawarmaShawarmaBeefIngredients =
            new()
            {
                stockItemBigPita, stockItemBeef, stockItemTomatoes, stockItemSalad, 
                stockItemPickles, stockItemKetchup, stockItemMayonnaise, stockItemPotatoes
            };
        List<StockItem> shawarmaShawarmaMixIngredients =
            new()
            {
                stockItemBigPita, stockItemBeef, stockItemTomatoes, stockItemSalad, stockItemChicken,
                stockItemPickles, stockItemKetchup, stockItemMayonnaise, stockItemPotatoes
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
            new() { foodItemShawarmaShaormaCuCeva, foodItemShawarmaComfortShaorma, foodItemShawarmaChicken, foodItemShawarmaBeef, foodItemShawarmaMix };
        List<FoodItem> ketchupContainingFoodItems =
            new() { foodItemShawarmaComfortShaorma, foodItemShawarmaShaormaSaracului, foodItemShawarmaChicken, foodItemShawarmaBeef, foodItemShawarmaMix };
        List<FoodItem> spicyMayonnaiseContainingFoodItems =
            new() { foodItemShawarmaShaormaCuCeva };
        List<FoodItem> samuraiSauceContainingFoodItems =
            new() { foodItemPizzaAmericanSpicy };
        List<FoodItem> chickenContainingFoodItems =
            new()
            {
                foodItemShawarmaComfortShaorma, foodItemPizzaChickenCorn, foodItemPizzaCalifornia,
                foodItemPizzaMeatLovers, foodItemShawarmaChicken, foodItemShawarmaMix
            };
        List<FoodItem> beefContainingFoodItems =
            new()
            {
                foodItemShawarmaShaormaSaracului, foodItemShawarmaShaormaCuCeva, foodItemPizzaSuprema,
                foodItemPizzaMeatLovers, foodItemShawarmaBeef, foodItemShawarmaMix
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
            new() { foodItemShawarmaShaormaCuCeva, foodItemShawarmaShaormaSaracului, foodItemShawarmaChicken, foodItemShawarmaBeef, foodItemShawarmaMix };
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
                foodItemPizzaAmericanSpicy, foodItemPizzaChickenCorn, foodItemShawarmaChicken, foodItemShawarmaBeef, foodItemShawarmaMix
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
            new() { foodItemShawarmaShaormaCuCeva, foodItemShawarmaComfortShaorma, foodItemShawarmaChicken, foodItemShawarmaBeef, foodItemShawarmaMix };
        List<FoodItem> picklesContainingFoodItems =
            new() { foodItemShawarmaShaormaSaracului, foodItemShawarmaComfortShaorma, foodItemShawarmaChicken, foodItemShawarmaBeef, foodItemShawarmaMix };
        List<FoodItem> saladContainingFoodItems =
            new() { foodItemShawarmaShaormaSaracului, foodItemShawarmaChicken, foodItemShawarmaBeef, foodItemShawarmaMix };

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
        foodItemShawarmaChicken.Ingredients = shawarmaShawarmaChickenIngredients;
        foodItemShawarmaBeef.Ingredients = shawarmaShawarmaBeefIngredients;
        foodItemShawarmaMix.Ingredients = shawarmaShawarmaMixIngredients;

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
            context.Categories.Add(categoryDessert);
            context.Categories.Add(categoryBeverage);
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
            context.FoodItems.Add(foodItemShawarmaChicken);
            context.FoodItems.Add(foodItemShawarmaBeef);
            context.FoodItems.Add(foodItemShawarmaMix);
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