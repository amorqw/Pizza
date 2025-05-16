create table Role (
                      id_role UUID PRIMARY KEY ,
                      role_name VARCHAR(50) NOT NULL
);

create table Users (
                       id_user UUID PRIMARY KEY,
                       first_name VARCHAR(50) NOT NULL,
                       last_name VARCHAR(50) NOT NULL,
                       middle_name VARCHAR(50),
                       phone VARCHAR(20),
                       email VARCHAR(100) NOT NULL UNIQUE,
                       password VARCHAR(255) NOT NULL,
                       id_role UUID NOT NULL
);

create table Service (
                         id_service UUID PRIMARY KEY,
                         service_name VARCHAR(50),
                         cost INT NOT NULL
);

create table RepairOrder(
                            id_order UUID PRIMARY KEY,
                            id_service UUID NOT NULL,
                            id_user UUID NOT NULL,
                            car_registration_number VARCHAR(10) NOT NULL,
                            car_model VARCHAR(20) NOT NULL,
                            problem_description TEXT,
                            datetime TIMESTAMP,
                            status VARCHAR(20)
);




