CREATE VIEW EventRates
	AS SELECT r.Id, r.Event_id, u.UserName, r.Rating_Score, r.Comments FROM Rates r JOIN AspNetUsers u ON r.Reviewer_id = u.Id;
