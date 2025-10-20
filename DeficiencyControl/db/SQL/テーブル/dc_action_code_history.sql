-- Table: dc_action_code_history

-- DROP TABLE dc_action_code_history;

CREATE TABLE dc_action_code_history
(
  action_code_history_id serial NOT NULL,
  comment_item_id integer,
  action_code_id integer,
  action_code_text character varying,
  order_no integer,
  
  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_dc_action_code_history PRIMARY KEY (action_code_history_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE dc_action_code_history
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE dc_action_code_history
  IS 'アクションコード履歴テーブル';

-- Trigger: tr_dc_action_code_history on dc_action_code_history

-- DROP TRIGGER tr_dc_action_code_history ON dc_action_code_history;

CREATE TRIGGER tr_dc_action_code_history
  BEFORE INSERT OR UPDATE
  ON dc_action_code_history
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();

