/*--Part 1
id INT, 
name VARCHAR(200),
employerid INT

--Part 2
SELECT name, location
FROM employers
WHERE location = "Saint Louis City";

--Part 3 
SELECT *
FROM skills
INNER JOIN jobskills ON skills.Id = jobskills.Id
WHERE skills.Id IS NOT NULL
ORDER BY Name  ASC;
*/