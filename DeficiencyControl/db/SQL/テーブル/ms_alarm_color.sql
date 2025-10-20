-- Table: ms_alarm_color

-- DROP TABLE ms_alarm_color;

CREATE TABLE ms_alarm_color
(
  alarm_color_id serial NOT NULL,
  day_count integer,
  color_r integer,
  color_g integer,
  color_b integer,
  comment character varying,
  
  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_ms_alarm_color PRIMARY KEY (alarm_color_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE ms_alarm_color
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE ms_alarm_color
  IS 'アラーム色マスタ';

-- Trigger: tr_ms_alarm_color on ms_alarm_color

-- DROP TRIGGER tr_ms_alarm_color ON ms_alarm_color;

CREATE TRIGGER tr_ms_alarm_color
  BEFORE INSERT OR UPDATE
  ON ms_alarm_color
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();