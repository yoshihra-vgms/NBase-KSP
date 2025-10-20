-- Table: ms_moi_status

-- DROP TABLE ms_moi_status;

CREATE TABLE ms_moi_status
(
  moi_status_id serial NOT NULL,
  moi_status_name character varying,

  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_ms_moi_status PRIMARY KEY (moi_status_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE ms_moi_status
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE ms_moi_status
  IS '検船状態マスタ';

-- Trigger: tr_ms_moi_status on ms_moi_status

-- DROP TRIGGER tr_ms_moi_status ON ms_moi_status;

CREATE TRIGGER tr_ms_moi_status
  BEFORE INSERT OR UPDATE
  ON ms_moi_status
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();