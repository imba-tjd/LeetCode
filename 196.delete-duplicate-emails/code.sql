delete
from Person
where id not in (select * from (
    select min(id) from Person
    group by email
) t);

-- Solution2
with t as (
    select min(id) from Person
    group by email
)
delete
from Person
where id not in (select * from t);
