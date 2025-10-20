--元データ全削除
DELETE FROM ms_schedule_kind_detail;

--シーケンスリセット
SELECT SETVAL ('ms_schedule_kind_detail_schedule_kind_detail_id_seq', '1', false);

--Insert
INSERT INTO ms_schedule_kind_detail(schedule_kind_id, schedule_kind_detail_name, color_r, color_g, color_b) VALUES (1, 'JXTG検船', 255, 174, 201);
INSERT INTO ms_schedule_kind_detail(schedule_kind_id, schedule_kind_detail_name, color_r, color_g, color_b) VALUES (1, '昭和シェル', 255, 174, 201);
INSERT INTO ms_schedule_kind_detail(schedule_kind_id, schedule_kind_detail_name, color_r, color_g, color_b) VALUES (1, '出光', 255, 174, 201);
INSERT INTO ms_schedule_kind_detail(schedule_kind_id, schedule_kind_detail_name, color_r, color_g, color_b) VALUES (1, 'STASCO', 255, 174, 201);
INSERT INTO ms_schedule_kind_detail(schedule_kind_id, schedule_kind_detail_name, color_r, color_g, color_b) VALUES (2, 'SMC(更新)', 181, 230, 29);
INSERT INTO ms_schedule_kind_detail(schedule_kind_id, schedule_kind_detail_name, color_r, color_g, color_b) VALUES (2, 'SMC(中間)', 181, 230, 29);
INSERT INTO ms_schedule_kind_detail(schedule_kind_id, schedule_kind_detail_name, color_r, color_g, color_b) VALUES (2, 'ISSC(更新)', 181, 230, 29);
INSERT INTO ms_schedule_kind_detail(schedule_kind_id, schedule_kind_detail_name, color_r, color_g, color_b) VALUES (2, 'ISSC(中間)', 181, 230, 29);
INSERT INTO ms_schedule_kind_detail(schedule_kind_id, schedule_kind_detail_name, color_r, color_g, color_b) VALUES (3, 'SMS', 200, 200, 200);
INSERT INTO ms_schedule_kind_detail(schedule_kind_id, schedule_kind_detail_name, color_r, color_g, color_b) VALUES (3, 'ISPS', 200, 200, 200);
INSERT INTO ms_schedule_kind_detail(schedule_kind_id, schedule_kind_detail_name, color_r, color_g, color_b) VALUES (4, '定検', 153, 217, 234);
INSERT INTO ms_schedule_kind_detail(schedule_kind_id, schedule_kind_detail_name, color_r, color_g, color_b) VALUES (4, '年次(AF)', 153, 217, 234);
INSERT INTO ms_schedule_kind_detail(schedule_kind_id, schedule_kind_detail_name, color_r, color_g, color_b) VALUES (4, '中間', 153, 217, 234);
INSERT INTO ms_schedule_kind_detail(schedule_kind_id, schedule_kind_detail_name, color_r, color_g, color_b) VALUES (4, '年次', 153, 217, 234);

INSERT INTO ms_schedule_kind_detail(schedule_kind_id, schedule_kind_detail_name, color_r, color_g, color_b) VALUES (5, '外航', -1, -1, -1);
INSERT INTO ms_schedule_kind_detail(schedule_kind_id, schedule_kind_detail_name, color_r, color_g, color_b) VALUES (5, '内航', -1, -1, -1);
INSERT INTO ms_schedule_kind_detail(schedule_kind_id, schedule_kind_detail_name, color_r, color_g, color_b) VALUES (6, '海務安全G', -1, -1, -1);
INSERT INTO ms_schedule_kind_detail(schedule_kind_id, schedule_kind_detail_name, color_r, color_g, color_b) VALUES (6, '工務G', -1, -1, -1);
INSERT INTO ms_schedule_kind_detail(schedule_kind_id, schedule_kind_detail_name, color_r, color_g, color_b) VALUES (6, '船員G', -1, -1, -1);
INSERT INTO ms_schedule_kind_detail(schedule_kind_id, schedule_kind_detail_name, color_r, color_g, color_b) VALUES (7, '神戸本社 工務G', -1, -1, -1);
INSERT INTO ms_schedule_kind_detail(schedule_kind_id, schedule_kind_detail_name, color_r, color_g, color_b) VALUES (7, '神戸本社 船員G', -1, -1, -1);
INSERT INTO ms_schedule_kind_detail(schedule_kind_id, schedule_kind_detail_name, color_r, color_g, color_b) VALUES (7, '神戸本社 管理G', -1, -1, -1);
INSERT INTO ms_schedule_kind_detail(schedule_kind_id, schedule_kind_detail_name, color_r, color_g, color_b) VALUES (7, '東京支店 管理G', -1, -1, -1);
INSERT INTO ms_schedule_kind_detail(schedule_kind_id, schedule_kind_detail_name, color_r, color_g, color_b) VALUES (7, '東京支店 海務安全G', -1, -1, -1);
INSERT INTO ms_schedule_kind_detail(schedule_kind_id, schedule_kind_detail_name, color_r, color_g, color_b) VALUES (7, '東京支店 船舶', -1, -1, -1);
INSERT INTO ms_schedule_kind_detail(schedule_kind_id, schedule_kind_detail_name, color_r, color_g, color_b) VALUES (7, '出張所 徳山出張所', -1, -1, -1);
INSERT INTO ms_schedule_kind_detail(schedule_kind_id, schedule_kind_detail_name, color_r, color_g, color_b) VALUES (8, '内航', -1, -1, -1);
INSERT INTO ms_schedule_kind_detail(schedule_kind_id, schedule_kind_detail_name, color_r, color_g, color_b) VALUES (8, '外航', -1, -1, -1);





