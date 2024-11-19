DELIMITER $$

CREATE PROCEDURE GetAllRoles()
BEGIN 
	Select * From AspNetUsersRoles ;
END $$
DELIMITER ;