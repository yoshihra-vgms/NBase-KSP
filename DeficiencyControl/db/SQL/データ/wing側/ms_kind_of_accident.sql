--元データ全削除
DELETE FROM ms_kind_of_accident;

--シーケンスリセット
SELECT SETVAL ('ms_kind_of_accident_kind_of_accident_id_seq', '1', false);

--Insert
INSERT INTO ms_kind_of_accident(kind_of_accident_name) VALUES ('人身事故');
INSERT INTO ms_kind_of_accident(kind_of_accident_name) VALUES ('疾病');
INSERT INTO ms_kind_of_accident(kind_of_accident_name) VALUES ('衝突');
INSERT INTO ms_kind_of_accident(kind_of_accident_name) VALUES ('乗揚');
INSERT INTO ms_kind_of_accident(kind_of_accident_name) VALUES ('浸水');
INSERT INTO ms_kind_of_accident(kind_of_accident_name) VALUES ('沈没');
INSERT INTO ms_kind_of_accident(kind_of_accident_name) VALUES ('火災');
INSERT INTO ms_kind_of_accident(kind_of_accident_name) VALUES ('爆発');
INSERT INTO ms_kind_of_accident(kind_of_accident_name) VALUES ('漏油、混油');
INSERT INTO ms_kind_of_accident(kind_of_accident_name) VALUES ('設備等接触');
INSERT INTO ms_kind_of_accident(kind_of_accident_name) VALUES ('重要機器故障');
INSERT INTO ms_kind_of_accident(kind_of_accident_name) VALUES ('法令違反');
INSERT INTO ms_kind_of_accident(kind_of_accident_name) VALUES ('その他');


