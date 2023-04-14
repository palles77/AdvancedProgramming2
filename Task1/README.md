-----------------------------------

Tasks to accomplish:

1. Add the following new properties to the Student entity:
a) Gender
b) Place of Birth
c) Place of Residence (town, city or village data would be entered here)
d) Address Line 1
e) Address Line 2
f) Postal Code
These properties should be renderred in Student related controls and views.

2. Rename Subject entity to Course and modify it such that it has the following properties:
* Course Code (string)
* Title (string)
* Instructor (string)
* Schedule (string)
* Description (string)
* Credits (integer)
* Department (string)
* Prerequisites (list of strings)

3. Add two chosen of the following entities into database and the GUI. 
Imporant notes:
* Remember that you should use a service for data manipulation. 
* The service you write should use templates.
* All the data types and services should have corresponding interfaces.

a) FacultyMember:
* faculty_id (string)
* name (string)
* age (integer)
* gender (string)
* department (string)
* position (string)
* email (string)
* office_room_number (string)

b) Book:
* book_id (string)
* title (string)
* author (string)
* publisher (string)
* publication_date (date)
* isbn (string)
* genre (string)
* description (string)

c) ResearchProject:
* project_id (string)
* title (string)
* description (string)
* team_members (list of strings)
* supervisor (string)
* start_date (date)
* end_date (date)
* budget (float)

d) Classroom:
* classroom_id (string)
* location (string)
* capacity (integer)
* available_seats (integer)
* projector (boolean)
* whiteboard (boolean)
* microphone (boolean)
* description (string)

e) Library:
* name (string)
* address (string)
* number_of_floors (integer)
* number_of_rooms (integer)
* description (string)
* facilities (list of strings)
* librarian (string)

f) AthleticsFacility:

* facility_id (string)
* name (string)
* location (string)
* type (string)
* description (string)
* availability (list of strings)
* equipment (list of strings)
* capacity (integer)

g) StudentOrganization:
* org_id (string)
* name (string)
* advisor (string)
* president (string)
* description (string)
* meeting_schedule (string)
* membership (list of strings)
* email (string)

h) Exam:
* exam_id (string)
* course_code (string)
* date (date)
* start_time (time)
* end_time (time)
* location (string)
* description (string)
* professor (string)

4. Add unit tests for the services created in task 4 and 5:
* Unit tests should be written in a separate library with a suffix .Tests compliant with the name of the project that is under tests.
* You should use mocking framework of your choice for doing so.

8. Add support for one of the chosen many to many relationships. Make sure you have a support for the entities used in such mapping.

a) FacultyMembers and ResearchProjects (a faculty member could supervise multiple research projects, and a research project could have multiple faculty members on its team)

b) Books and Courses (a book could be used in multiple courses, and a course could require multiple books)

c) StudentOrganizations and Students (an organization could have multiple members, and a student could belong to multiple organizations)

d) Exams and Courses (a course could have multiple exams, and an exam could be given in multiple courses)

e) AthleticsFacilities and FacultyMembers (a faculty member could use multiple facilities for their research or recreation, and a facility could be used by multiple faculty members)

f) ResearchProjects and Students (a research project could have multiple undergraduate students working on it, and a student could work on multiple research projects)

g) Books and Libraries (a library could have multiple copies of the same book, and a book could be found in multiple libraries)

h) StudentOrganizations and Students (a graduate student could belong to multiple organizations, and an organization could have multiple graduate student members)

i) Exams and Students (a graduate student could take multiple exams, and an exam could be taken by multiple graduate students)

j) AthleticsFacilities and Students (an undergraduate student could use multiple facilities for their recreation, and a facility could be used by multiple undergraduate students)

9. Add support for one more entity from task 6.

10. Add support for one more entity from task 8.

-----------------------------------

Grading

a) 'dst' - tasks from 1 to 6.
b) 'dst plus' - tasks from 1 to 7.
b) 'db' - tasks from 1 to 8.
b) 'db plus' - tasks from 1 to 9.
b) 'bdb' - tasks from 1 to 10.

-----------------------------------

Solutions should be sent to email address as links to a repository in GitHub before 2023/09/15: leslaw dot pawlaczyk at chorzow dot wsb dot pl

-----------------------------------

Last Modified

2023/04/13
