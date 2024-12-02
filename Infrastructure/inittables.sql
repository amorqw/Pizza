create table Pizzas
(
    PizzaId     serial primary key,
    NamePizza   varchar(50),
    Description varchar(200),
    Price       int,
    Size        varchar(50),
    Available   bool
);

create table Ingredients
(
    IngredientId   serial primary key,
    NameIngredient varchar(50)
);

create table PizzaIngredients
(
    PizzaId      int references Pizzas (PizzaId) on delete cascade,
    IngredientId int references Ingredients (IngredientId) on delete cascade,
    primary key (PizzaId, IngredientId)
);

create table Users
(
    UserId      serial primary key,
    FirstName   varchar(50),
    LastName    varchar(50),
    Email       varchar(50),
    PhoneNumber varchar(50),
    Password    varchar(50),
    Role        varchar(50)
);

create table Orders
(
    OrderId        serial primary key,
    UserId         int references Users (UserId) on delete cascade,
    OrderData      TIMESTAMP not null default current_timestamp,
    TotalPrice     int,
    Status         varchar(50),
    DeliveryAdress text
);

create table OrderItems
(
    OrderItemId serial primary key,
    OrderId     int references Orders (OrderId) on delete cascade,
    PizzaId     int references Pizzas (PizzaId) on delete cascade,
    Quantity    int not null,
    ItemPrice   int not null
);

create table Staff
(
    StaffId   serial primary key,
    FirstName varchar(50),
    LastName  varchar(50),
    Position  varchar(50),
    HireDate  TIMESTAMP not null default current_timestamp
);

create table Reviews
(
    ReviewId   serial primary key,
    PizzaId    int references Pizzas (PizzaId),
    UserId     int references Users (UserId),
    Rating     int,
    Comment    text,
    ReviewDate TIMESTAMP not null default current_timestamp
);
