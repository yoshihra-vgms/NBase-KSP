-- Table: dc_operation_history

-- DROP TABLE dc_operation_history;

CREATE TABLE dc_operation_history
(
  dc_operation_history_id serial NOT NULL,
  
  ms_user_id character varying(40),
  host character varying,
  ms_user_operation_kind_id integer,
  date timestamp without time zone,
  
  
  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  
  CONSTRAINT pk_dc_operation_history PRIMARY KEY (dc_operation_history_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE dc_operation_history
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE dc_operation_history
  IS 'ユーザー操作履歴テーブル';

-- Trigger: tr_dc_operation_history on dc_operation_history

-- DROP TRIGGER tr_dc_operation_history ON dc_operation_history;

CREATE TRIGGER tr_dc_operation_history
  BEFORE INSERT OR UPDATE
  ON dc_operation_history
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();

