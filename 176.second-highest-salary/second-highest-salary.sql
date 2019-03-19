/* Write your T-SQL query statement below */
select MAX(Salary) as SecondHighestSalary
from Employee
where Salary < (
    select MAX(Salary)
from Employee);

select top 1 height from users where height not in (select MAX(height) from users) order by height desc;

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

# limit 2,1 从第三条数据开始查询，取一条数据
# limit 2 offset 1 从第二条数据开始查询两条数据
*/
