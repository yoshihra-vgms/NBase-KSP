--元データ全削除 
DELETE FROM ms_attachment_type;

--シーケンスリセット
SELECT SETVAL ('ms_attachment_type_attachment_type_id_seq', '1', false);

--Insert
INSERT INTO ms_attachment_type(attachment_type_name) VALUES('CI_Report_Record');
INSERT INTO ms_attachment_type(attachment_type_name) VALUES('CI_ActionTakenByVessel');
INSERT INTO ms_attachment_type(attachment_type_name) VALUES('CI_ActionTakenByCompany');
INSERT INTO ms_attachment_type(attachment_type_name) VALUES('CI_ClassInvolved');
INSERT INTO ms_attachment_type(attachment_type_name) VALUES('CI_CorrectiveAction');

INSERT INTO ms_attachment_type(attachment_type_name) VALUES('AC_Accident');
INSERT INTO ms_attachment_type(attachment_type_name) VALUES('AC_SpotReport');
INSERT INTO ms_attachment_type(attachment_type_name) VALUES('AC_Progress');
INSERT INTO ms_attachment_type(attachment_type_name) VALUES('AC_CauseOfAccident');
INSERT INTO ms_attachment_type(attachment_type_name) VALUES('AC_PreventiveAction');
INSERT INTO ms_attachment_type(attachment_type_name) VALUES('AC_Reports');

INSERT INTO ms_attachment_type(attachment_type_name) VALUES('MO_InspectionReport');
INSERT INTO ms_attachment_type(attachment_type_name) VALUES('MO_1stComemnt');
INSERT INTO ms_attachment_type(attachment_type_name) VALUES('MO_2ndtComemnt');
