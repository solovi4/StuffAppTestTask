create table Department
(
    Id serial primary key,
    Title varchar(100) not null,
    Floor int not null
);

insert into department (title, floor)
values ('Мобильная разработка', 1),
       ('Веб разработка', 2),
       ('Бэкенд разработка', 3)
