/* Write your T-SQL query statement below */
select MAX(Salary) as SecondHighestSalary
from Employee
where Salary < (
    select MAX(Salary)
from Employee);

/* 最快的mssql: */

SELECT (
SELECT DISTINCT Salary as SecondHighestSalary
    from Employee
    Order by Salary desc
offset 1 ROWS
FETCH NEXT 1 ROWS ONLY) as SecondHighestSalary

/* mysql:
# Write your MySQL query statement below
select (
select distinct Salary
from Employee
order by Salary desc
limit 1,1
    ) SecondHighestSalary
*/
