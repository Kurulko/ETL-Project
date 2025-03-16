# ETL Project

A simple ETL project in CLI that inserts data from a CSV into a single, flat table

--- 

## Table of Contents

- [Test Assessment](#test-assessment)
- [Installation](#installation)
- [Questions and Answers](#questions-and-answers)
  
---

## Test Assessment

The goal of this task is to implement a simple ETL project in CLI that inserts data from a CSV into a single, flat table. 

### Objectives

1. Import the data from the CSV into an MS SQL table. We only want to store the following columns:
    - `tpep_pickup_datetime`
    - `tpep_dropoff_datetime`
    - `passenger_count`
    - `trip_distance`
    - `store_and_fwd_flag`
    - `PULocationID`
    - `DOLocationID`
    - `fare_amount`
    - `tip_amount`
2. Set up a SQL Server database (local or cloud-based, as per your convenience).
3. Design a table schema that will hold the processed data; make sure you are using the proper data types.
4. Users of the table will perform the following queries; ensure your schema is optimized for them:
    - Find out which `PULocationId` (Pick-up location ID) has the highest tip_amount on average.
    - Find the top 100 longest fares in terms of `trip_distance`.
    - Find the top 100 longest fares in terms of time spent traveling.
    - Search, where part of the conditions is `PULocationId`.
5. Implement efficient bulk insertion of the processed records into the database.
6. Identify and remove any duplicate records from the dataset based on a combination of `pickup_datetime`, `dropoff_datetime`, and `passenger_count`. Write all removed duplicates into a `duplicates.csv` file.
7. For the `store_and_fwd_flag` column, convert any 'N' values to 'No' and any 'Y' values to 'Yes'.
8. Ensure that all text-based fields are free from leading or trailing whitespace.
9. Assume your program will be used on much larger data files. Describe in a few sentences what you would change if you knew it would be used for a 10GB CSV input file.
10. (nice to have) The input data is in the EST timezone. Convert it to UTC when inserting into the DB.

### Requirements

- Use C# as the primary programming language.
- Efficiency of data insertion into SQL Server.
- Assume the data comes from a potentially unsafe source.

## Installation

### Step 1: **Clone the repository**
   ```
   git clone https://github.com/Kurulko/ETL-Project.git
   cd ETL-Project
  ```

### Step 2. **Install the necessary dependencies**
```
dotnet restore
```

### Step 3. **Set up the database (SQL Server)**
Open SQL Server Management Studio (SSMS) or any SQL management tool.

#### a. Create the database:
```
CREATE DATABASE ETL_db;
GO
```

#### b.  Apply the necessary SQL schema:
```
CREATE TABLE Vendors (
	VendorID INT IDENTITY PRIMARY KEY,
  tpep_pickup_datetime DATETIME NOT NULL,
  tpep_dropoff_datetime DATETIME NOT NULL,
  passenger_count INT NULL,
  trip_distance FLOAT NOT NULL,
  store_and_fwd_flag VARCHAR(3) NULL,
  fare_amount DECIMAL(5,2) NOT NULL,
  tip_amount DECIMAL(5,2) NOT NULL,
	PULocationID INT NOT NULL,
  DOLocationID INT NOT NULL,

	CONSTRAINT CK_Vendors_tpep_dropoff_datetime CHECK (tpep_dropoff_datetime > tpep_pickup_datetime),
  CONSTRAINT CK_Vendors_passenger_count CHECK (passenger_count IS NULL OR passenger_count >= 0),
  CONSTRAINT CK_Vendors_trip_distance CHECK (trip_distance >= 0),
  CONSTRAINT CK_Vendors_store_and_fwd_flag CHECK (store_and_fwd_flag IS NULL OR store_and_fwd_flag IN ('Yes', 'No')),
  CONSTRAINT CK_Vendors_fare_amount CHECK (fare_amount >= 0),
  CONSTRAINT CK_Vendors_tip_amount CHECK (tip_amount >= 0),
);
GO
```

### Step 4. **Configure appsettings.json**

#### 1. Create the appsettings.json file in the project's root folder.
#### 2. Add your sensitive settings (e.g., connection strings, CSV settings) to appsettings.json:
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb; Database=ETL_db; Trusted_Connection=True;"
  },
  "CsvSettings": {
    "DataFilePath": "path-to-sample-data-file\\sample-cab-data.csv",
    "DuplicatesFilePath": "path-to-duplicates-data-file\\duplicates.csv"
  }
}
```

### Step 5. **Run the project**
In the project's root folder
```
dotnet run
```

### Step 6. **Execute SQL tasks**
#### 1. Open SQL Server Management Studio (SSMS) or any SQL management tool.
#### 2. Execute SQL tasks.

##### Finding out which `PULocationId` (Pick-up location ID) has the highest tip_amount on average
```
WITH AvgTipAmounts AS (
  SELECT PULocationID, AVG(tip_amount) as avg_tip_amount 
	FROM Vendors
	GROUP BY PULocationID
)
SELECT PULocationID, avg_tip_amount as highest_avg_tip_amount FROM AvgTipAmounts
WHERE avg_tip_amount = (
	SELECT MAX(avg_tip_amount) FROM AvgTipAmounts
);
```

##### Finding the top 100 longest fares in terms of `trip_distance`
```
SELECT TOP 100 trip_distance FROM Vendors
ORDER BY trip_distance DESC;
```

##### Finding the top 100 longest fares in terms of time spent traveling
```
SELECT TOP 100 tpep_pickup_datetime, tpep_dropoff_datetime, DATEDIFF(SECOND, tpep_pickup_datetime, tpep_dropoff_datetime) AS time_spent_traveling_in_seconds 
FROM Vendors
ORDER BY time_spent_traveling_in_seconds DESC;
```

##### Searching, where part of the conditions is `PULocationId`
```
SELECT * FROM Vendors
WHERE PULocationId = 193;
```

## Questions and Answers

**№1**

**Question**: 
Assume your program will be used on much larger data files. Describe in a few sentences what you would change if you knew it would be used for a 10GB CSV input file.

**Answer**:
I would use the `SqlBulkCopy` class from the `Microsoft.Data.SqlClient` namespace to use bulk insert operations, I would also implement streaming processing (e.g. reading the file in smaller portions) instead of loading the entire CSV file into memory, and last but not least, I would optimize the database schema by adding relevant indexes.

---

**№2**

**Question**: 
Number of rows in your table after running the program

**Answer**: 

Number of rows after running: **29889** (in db)

Number of duplicate rows: **111** (in duplicates.csv file)

