create table Employee
(
    Id serial primary key,
    Name varchar(100) not null,
    Surname varchar(100) not null,
    Age int not null,
    Gender int not null,
    DepartmentId int not null,
    constraint fk_department
        foreign key (DepartmentId) references department(id),
    constraint fk_gender
        foreign key (Gender) references gender(id)
)