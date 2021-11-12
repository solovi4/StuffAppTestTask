create table Experience
(
    EmployeeId int,
    ProgramLanguageId int,
    primary key (EmployeeId, ProgramLanguageId),
    foreign key (EmployeeId) references employee(id),
    foreign key (ProgramLanguageId) references programlanguage(id)
)