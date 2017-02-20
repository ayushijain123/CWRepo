
   CREATE PROCEDURE [dbo].[GetStudentGrades]
   @StudentID int
   AS
   SELECT EnrollmentID, Grade, CourseID, StudentID FROM dbo.StudentGrade 
   WHERE StudentID = @StudentID