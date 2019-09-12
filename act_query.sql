SELECT * FROM AspNetUsers;

SELECT * FROM AspNetRoles;

SELECT * FROM AspNetUserLogins;

SELECT * FROM AspNetUserClaims;

INSERT INTO AspNetRoles(Id, Name) VALUES (1001, 'Administrator');

INSERT INTO AspNetUserRoles(RoleId, UserId) 
VALUES (
	1001,
	(SELECT id FROM AspNetUsers WHERE Email = 'jerrycheung186@gmail.com')
);

COMMIT;