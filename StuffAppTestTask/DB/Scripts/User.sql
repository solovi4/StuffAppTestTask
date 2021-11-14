create table users
(
    id serial primary key,
    login varchar(50) not null,
    password varchar(50) not null,
    lastactiondate timestamp
);

insert into users (login, password)
values ('admin', '1234')