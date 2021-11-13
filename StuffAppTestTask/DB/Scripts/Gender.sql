create table Gender
(
    Id serial primary key,
    Title varchar(100)
);

insert into Gender (Title)
values ('Мужчина'), ('Женщина'), ('Все сложно')