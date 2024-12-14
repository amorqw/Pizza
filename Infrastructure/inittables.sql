CREATE TABLE Pizzerias
(
    PizzeriaId    SERIAL PRIMARY KEY,
    title          VARCHAR(255) NOT NULL,
    rating         DECIMAL(3, 2) CHECK (rating >= 0 AND rating <= 5),
    address        VARCHAR(255) NOT NULL,
    courier_amount INT CHECK (courier_amount >= 0)
);

CREATE TABLE Pizzas
(
    PizzaId    SERIAL PRIMARY KEY,
    title       VARCHAR(255) NOT NULL,
    description TEXT,
    price       INT,
    size        VARCHAR(10) CHECK (size IN ('small', 'medium', 'large')),
    receipt     TEXT 
);

CREATE TABLE PizzasAvailable
(
    PizzeriaId INT REFERENCES Pizzerias (pizzeriaid) ON DELETE CASCADE,
    PizzaId    INT REFERENCES Pizzas (pizzaid) ON DELETE CASCADE,
    available   BOOLEAN NOT NULL,
    PRIMARY KEY (pizzeriaid, PizzaId)
);



CREATE TABLE Couriers
(
    StaffId SERIAL PRIMARY KEY,
    FirstName       VARCHAR(255) NOT NULL,
    LastName    VARCHAR(255) NOT NULL,
    HireDate  TIMESTAMP not null default current_timestamp
);

CREATE TABLE Orders
(
    OrderId       SERIAL PRIMARY KEY,
    UserId        INT REFERENCES Users (UserId) ON DELETE CASCADE,
    StaffId     INT          REFERENCES Couriers (StaffId) ON DELETE SET NULL,
    date           TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status         VARCHAR(50)  NOT NULL,
    address        VARCHAR(255) NOT NULL,
    payment_method VARCHAR(50)  NOT NULL
);

CREATE TABLE OrderItems
(
    OrderId INT REFERENCES Orders (OrderId) ON DELETE CASCADE,
    PizzaId INT REFERENCES Pizzas (PizzaId) ON DELETE CASCADE,
    amount   INT CHECK (amount > 0),
    PRIMARY KEY (OrderId, PizzaId)
);

CREATE TABLE Reviews
(
    PizzaId    INT REFERENCES Pizzas (pizzaid) ON DELETE CASCADE,
    UserId     INT REFERENCES Users (UserId) ON DELETE CASCADE,
    OrderId    INT REFERENCES Orders (OrderId) ON DELETE CASCADE,
    ReviewDate TIMESTAMP not null default current_timestamp,
    comment     TEXT,
    rating      DECIMAL(2, 1) CHECK (rating >= 0 AND rating <= 5),
    PRIMARY KEY (PizzaId, UserId, OrderId)
);

CREATE TABLE Users
(
    UserId  SERIAL PRIMARY KEY,
    password VARCHAR  NOT NULL,
    name     VARCHAR(255)        NOT NULL,
    surname  VARCHAR(255)        NOT NULL,
    email    VARCHAR(255) UNIQUE NOT NULL,
    phone    VARCHAR(200) UNIQUE,
    role  varchar
);
insert into couriers( FirstName, LastName) values (123,123)

