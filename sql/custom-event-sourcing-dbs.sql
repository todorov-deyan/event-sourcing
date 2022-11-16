CREATE SCHEMA IF NOT EXISTS custom_event_sourcing;

CREATE TABLE IF NOT EXISTS custom_event_sourcing."streams"
(
    "stream_id" uuid NOT NULL PRIMARY KEY,
    "type" TEXT NOT NULL,
    "created_at" TIMESTAMP default (now() at time zone 'utc')
);

CREATE TABLE IF NOT EXISTS custom_event_sourcing."events"
(
    "event_id" uuid NOT NULL PRIMARY KEY,
    "stream_id" uuid NOT NULL,
	"data" jsonb NOT NULL,
    "event_type" TEXT NOT NULL,
    "created_at" TIMESTAMP default (now() at time zone 'utc'),
	FOREIGN KEY (stream_id) REFERENCES custom_event_sourcing.streams (stream_id)
);
