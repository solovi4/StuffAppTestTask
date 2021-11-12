create table ProgramLanguage
(
    Id serial primary key,
    Title varchar(100) not null
);

insert into ProgramLanguage (title)
values ('c#'), ('c++'), ('Java'), ('JavaScript'), ('python')