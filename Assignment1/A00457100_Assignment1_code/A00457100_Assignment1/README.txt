Assignment #1
-------------------------------------------------------------------

The goal of the assignment is to simply combine 3 of these programs into a single
program that will recursively read a series of data files in CSV format and enter 
them into a single file.

1. Read the files from all the directories.
2. Validated the below scenarios :
   a) Check if there are exactly 10 fields in every file, if not reject the row/line
   b) Check if any of the fields are empty, if so then reject the row/line
   c) Noticed that some email IDs are Null@domain, but that doesnt seem invalid since null is still a text and can be a valid email id
3. After validation, write all the successfull rows/lines into one single file named as Output.txt
4. Write all the invalid records into one single file along with the reason for being invalid and their exact row number to a file named as Skipped_rows.txt
5. Added Date field to all the rows/lines as per their directory location
6. Created a log file to contain the number of valid rows, number of invalid rows, and the total execution time to the file Log_file.txt

IMPORTANT :
To make the program work, make sure to replace "/Users/muralitulluri/Documents/" with appropriate path where the source data files reside.