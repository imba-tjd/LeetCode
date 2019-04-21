/* Write your T-SQL query statement below */
select a.Name as Employee
from Employee a
join Employee b on a.ManagerId = b.Id
where a.Salary > b.Salary;

select a.Name as Employee
from Employee a
join Employee b on a.ManagerId = b.Id and
    a.Salary > b.Salary;

select Name as Employee
from Employee a
where Salary > (
    select Salary
    from Employee b
    where a.ManagerId = b.Id
)

select a.Name as Employee
from Employee a,
    Employee b
where a.ManagerId = b.Id and
    a.Salary > b.Salary
