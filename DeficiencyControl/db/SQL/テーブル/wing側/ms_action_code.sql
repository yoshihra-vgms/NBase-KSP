-- Table: ms_action_code

-- DROP TABLE ms_action_code;

CREATE TABLE ms_action_code
(
  action_code_id serial NOT NULL,
  action_code_name character varying,
  action_code_text character varying,


  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_ms_action_code PRIMARY KEY (action_code_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE ms_action_code
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE ms_action_code
  IS 'ActionCodeマスタ';

-- Trigger: tr_ms_action_code on ms_action_code

-- DROP TRIGGER tr_ms_action_code ON ms_action_code;

CREATE TRIGGER tr_ms_action_code
  BEFORE INSERT OR UPDATE
  ON ms_action_code
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();