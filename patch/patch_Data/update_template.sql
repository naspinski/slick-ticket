USE [SlickTicket]

ALTER TABLE dbo.comments
  ADD assigned_to INT NOT NULL REFERENCES sub_units(id) DEFAULT _REPLACE_;

ALTER TABLE dbo.comments
  ADD priority_id INT NOT NULL REFERENCES priority(id) DEFAULT 1;

ALTER TABLE dbo.comments
  ADD status_id INT NOT NULL REFERENCES statuses(id) DEFAULT 1;