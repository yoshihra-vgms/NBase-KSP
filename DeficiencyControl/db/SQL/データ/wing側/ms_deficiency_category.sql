--元データ全削除 
DELETE FROM ms_deficiency_category;

--シーケンスリセット
SELECT SETVAL ('ms_deficiency_category_deficiency_category_id_seq', '1', false);

--INSERT
INSERT INTO ms_deficiency_category(deficiency_category_no, deficiency_category_name) VALUES ('01100', '証書 及び 書類');
INSERT INTO ms_deficiency_category(deficiency_category_no, deficiency_category_name) VALUES ('02100', '船体構造');
INSERT INTO ms_deficiency_category(deficiency_category_no, deficiency_category_name) VALUES ('03100', '水密 及び 防火扉等');
INSERT INTO ms_deficiency_category(deficiency_category_no, deficiency_category_name) VALUES ('04100', '緊急時のシステム');
INSERT INTO ms_deficiency_category(deficiency_category_no, deficiency_category_name) VALUES ('05100', '通信設備');
INSERT INTO ms_deficiency_category(deficiency_category_no, deficiency_category_name) VALUES ('06100', '機器を含む荷役');
INSERT INTO ms_deficiency_category(deficiency_category_no, deficiency_category_name) VALUES ('07100', '消火設備');
INSERT INTO ms_deficiency_category(deficiency_category_no, deficiency_category_name) VALUES ('08100', '警報装置');
INSERT INTO ms_deficiency_category(deficiency_category_no, deficiency_category_name) VALUES ('09100', '労働環境');
INSERT INTO ms_deficiency_category(deficiency_category_no, deficiency_category_name) VALUES ('10100', '航海関係');
INSERT INTO ms_deficiency_category(deficiency_category_no, deficiency_category_name) VALUES ('11100', '救命機器');
INSERT INTO ms_deficiency_category(deficiency_category_no, deficiency_category_name) VALUES ('12100', '危機貨物');
INSERT INTO ms_deficiency_category(deficiency_category_no, deficiency_category_name) VALUES ('13100', '推進機器 及び 補機');
INSERT INTO ms_deficiency_category(deficiency_category_no, deficiency_category_name) VALUES ('14100', '海洋汚染防止');
INSERT INTO ms_deficiency_category(deficiency_category_no, deficiency_category_name) VALUES ('15100', '安全管理システム');
INSERT INTO ms_deficiency_category(deficiency_category_no, deficiency_category_name) VALUES ('18100', '労働条件');
INSERT INTO ms_deficiency_category(deficiency_category_no, deficiency_category_name) VALUES ('99100', 'その他');
INSERT INTO ms_deficiency_category(deficiency_category_no, deficiency_category_name) VALUES ('---', '不明');




