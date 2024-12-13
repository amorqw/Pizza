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
    FirstName   varchar,
    LastName    varchar,
    Email       varchar,
    PhoneNumber varchar,
    Password    varchar,
    Role        varchar
);

create table Orders
(
    OrderId        serial primary key,
    UserId         int references Users (UserId) ,
    OrderData      TIMESTAMP not null default current_timestamp,
    TotalPrice     int,
    Status         varchar(50),
    DeliveryAdress text
);

create table OrderItems
(
    OrderItemId serial primary key,
    OrderId     int references Orders (OrderId),
    PizzaId     int references Pizzas (PizzaId),
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
    PizzaId    int references Pizzas (PizzaId) on delete cascade,
    UserId     int references Users (UserId) on delete cascade,
    Rating     int,
    Comment    text,
    ReviewDate TIMESTAMP not null default current_timestamp
);
insert into Reviews(ReviewId, pizzaid,userid,rating,comment) 
values(1,4,5,2,2)