--元データ全削除 
DELETE FROM ms_action_code;

--シーケンスリセット
SELECT SETVAL ('ms_action_code_action_code_id_seq', '1', false);

--INSERT
INSERT INTO ms_action_code(action_code_name, action_code_text, delete_flag) VALUES ('10', '欠陥は是正された',  false);
INSERT INTO ms_action_code(action_code_name, action_code_text, delete_flag) VALUES ('15', '次の寄港地までに是正',  false);
INSERT INTO ms_action_code(action_code_name, action_code_text, delete_flag) VALUES ('16', '14日以内に欠陥を是正',  false);
INSERT INTO ms_action_code(action_code_name, action_code_text, delete_flag) VALUES ('17', '出港までに欠陥を是正',  false);
INSERT INTO ms_action_code(action_code_name, action_code_text, delete_flag) VALUES ('18', '3ヶ月以内に欠陥を是正',  false);
INSERT INTO ms_action_code(action_code_name, action_code_text, delete_flag) VALUES ('30', '拘留に値する欠陥',  false);
INSERT INTO ms_action_code(action_code_name, action_code_text, delete_flag) VALUES ('40', '次回米国入港までに是正(米国)',  false);
INSERT INTO ms_action_code(action_code_name, action_code_text, delete_flag) VALUES ('50', '30日以内に是正(米国)',  false);
INSERT INTO ms_action_code(action_code_name, action_code_text, delete_flag) VALUES ('99', 'その他の処置',  false);
INSERT INTO ms_action_code(action_code_name, action_code_text, delete_flag) VALUES ('不明', '処置不明',  false);



