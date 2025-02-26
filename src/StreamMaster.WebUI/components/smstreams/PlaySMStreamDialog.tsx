import SMPopUp, { type SMPopUpRef } from "@components/sm/SMPopUp";
import type { SMStreamDto } from "@lib/smAPI/smapiTypes";
import React, { useRef, useState, useEffect } from "react";
import mpegts from "mpegts.js";

interface PlaySMStreamProperties {
	readonly smStream: SMStreamDto;
}

interface MediaInfoDisplay {
	videoCodec?: string;
	audioCodec?: string;
	width?: number;
	height?: number;
	fps?: number;
	videoBitrate?: number;
	audioBitrate?: number;
}

const PlaySMStreamDialog = ({ smStream }: PlaySMStreamProperties) => {
	const videoRef = useRef<HTMLVideoElement>(null);
	const playerRef = useRef<mpegts.Player | null>(null);
	const popupRef = useRef<SMPopUpRef>(null);
	const [error, setError] = useState<string>("");
	const [isAudioDisabled, setIsAudioDisabled] = useState<boolean>(false);
	const [mediaInfo, setMediaInfo] = useState<MediaInfoDisplay>({});

	const destroyPlayer = () => {
		try {
			if (playerRef.current) {
				playerRef.current.pause();
				playerRef.current.unload();
				playerRef.current.detachMediaElement();
				playerRef.current.destroy();
				playerRef.current = null;
			}
			if (videoRef.current) {
				videoRef.current.src = "";
				videoRef.current.load();
			}
			setError("");
			setIsAudioDisabled(false);
			setMediaInfo({});
		} catch (e) {
			console.error("Error destroying player:", e);
		}
	};

	const dismissError = () => {
		setError("");
	};

	const initPlayer = (disableAudio = false) => {
		try {
			if (!videoRef.current) {
				console.error("No video element found");
				return;
			}

			if (!mpegts.isSupported()) {
				setError("Your browser does not support MPEG-TS playback");
				return;
			}

			destroyPlayer();
			setIsAudioDisabled(disableAudio);

			console.log(
				`Initializing mpegts.js player ${disableAudio ? "without audio" : "with audio"}`,
			);

			playerRef.current = mpegts.createPlayer(
				{
					type: "mpegts",
					url: smStream.Url,
					isLive: true,
					hasAudio: !disableAudio, // Disable audio if needed
					hasVideo: true,
				},
				{
					enableWorker: true,
					lazyLoad: false,
					liveBufferLatencyChasing: true,
					fixAudioTimestampGap: true,
					seekType: "range",
					reuseRedirectedURL: true,
					liveBufferLatencyMaxLatency: 3.0,
					liveBufferLatencyMinRemain: 0.5,
					// Add codec configurations
					videoCodec: "avc", // H.264
					audioCodec: disableAudio ? undefined : "aac", // Try with AAC instead of AC-3
				},
			);

			playerRef.current.attachMediaElement(videoRef.current);

			playerRef.current.on(mpegts.Events.ERROR, (errorType, errorDetail) => {
				console.error("MPEGTS Error:", errorType, errorDetail);

				// If error is related to audio codec or MSE, try to reinitialize without audio
				if (
					!disableAudio &&
					(errorType === "MediaError" ||
						errorDetail?.includes("audio") ||
						errorDetail?.includes("ac-3") ||
						errorDetail?.includes("Can't play type"))
				) {
					console.log(
						"Audio codec error detected, attempting to play without audio...",
					);
					destroyPlayer();
					initPlayer(true); // Reinitialize without audio
					return;
				}

				setError(`Playback Error: ${errorType} - ${errorDetail || ""}`);
			});

			playerRef.current.on(mpegts.Events.STATISTICS_INFO, (stats) => {
				if (stats.speed < 500) {
					console.warn("Low playback speed detected:", stats.speed);
				}

				// Update media info with statistics
				setMediaInfo((prevInfo) => ({
					...prevInfo,
					videoBitrate: stats.videoBitrate,
					audioBitrate: stats.audioBitrate,
				}));
			});

			playerRef.current.on(mpegts.Events.MEDIA_INFO, (mediaInfo) => {
				console.log("Media Info:", mediaInfo);

				// Extract and display codec information
				setMediaInfo({
					videoCodec: mediaInfo.videoCodec,
					audioCodec: disableAudio
						? "Disabled (AC-3 not supported)"
						: mediaInfo.audioCodec,
					width: mediaInfo.width,
					height: mediaInfo.height,
					fps: mediaInfo.fps,
					videoBitrate: mediaInfo.videoBitrate,
					audioBitrate: mediaInfo.audioBitrate,
				});
			});

			playerRef.current.load();

			if (videoRef.current) {
				videoRef.current.playsInline = true;
				videoRef.current.autoplay = true;
				videoRef.current.preload = "auto";
				videoRef.current.setAttribute("webkit-playsinline", "true");
				videoRef.current.setAttribute("x5-playsinline", "true");
				videoRef.current.setAttribute("x5-video-player-type", "h5");
				videoRef.current.setAttribute("x5-video-player-fullscreen", "true");
			}
		} catch (e: any) {
			console.error("Error initializing player:", e);

			if (
				!disableAudio &&
				e.message &&
				(e.message.includes("audio") ||
					e.message.includes("MediaSource") ||
					e.message.includes("codec"))
			) {
				console.log("Audio-related error detected, trying without audio");
				initPlayer(true);
				return;
			}

			setError(`Player initialization failed: ${e.message}`);
		}
	};

	useEffect(() => {
		let lastState = false;
		const interval = setInterval(() => {
			const isOpen = popupRef.current?.getOpen() || false;

			if (isOpen !== lastState) {
				console.log("Modal state changed:", isOpen);
				lastState = isOpen;

				if (isOpen) {
					console.log("Modal opened - initializing player");
					initPlayer(false); // Start with audio enabled
				} else {
					console.log("Modal closed - destroying player");
					destroyPlayer();
				}
			}
		}, 100);

		return () => {
			clearInterval(interval);
			destroyPlayer();
		};
	}, []);

	return (
		<SMPopUp
			ref={popupRef}
			buttonClassName="icon-green"
			buttonDisabled={!smStream}
			title="Play Stream"
			icon="pi-play"
			contentWidthSize="8"
			modal
			modalClosable
			showClose={false}
			info=""
			modalCentered
			noBorderChildren
			zIndex={11}
		>
			<div style={{ width: "100%", height: "100%", position: "relative" }}>
				<video
					ref={videoRef}
					controls
					style={{
						width: "100%",
						height: "100%",
						minHeight: "360px",
						backgroundColor: "#000",
					}}
				/>

				{/* Dismissible Error Message */}
				{error && (
					<div
						style={{
							position: "absolute",
							top: "50%",
							left: "50%",
							transform: "translate(-50%, -50%)",
							color: "white",
							background: "rgba(220,53,69,0.8)",
							padding: "10px 15px",
							borderRadius: "4px",
							maxWidth: "80%",
							display: "flex",
							flexDirection: "column",
							alignItems: "center",
						}}
					>
						<div style={{ marginBottom: "10px" }}>{error}</div>
						<button
							onClick={dismissError}
							style={{
								background: "rgba(255,255,255,0.2)",
								border: "1px solid rgba(255,255,255,0.4)",
								color: "white",
								padding: "5px 10px",
								borderRadius: "3px",
								cursor: "pointer",
								fontSize: "12px",
							}}
						>
							Dismiss
						</button>
					</div>
				)}

				{/* Media Info Display */}
				<div
					style={{
						position: "absolute",
						top: "10px",
						left: "10px",
						background: "rgba(0,0,0,0.6)",
						color: "white",
						padding: "8px",
						borderRadius: "4px",
						fontSize: "12px",
						maxWidth: "300px",
					}}
				>
					<div style={{ fontWeight: "bold", marginBottom: "5px" }}>
						Player: mpegts.js
					</div>

					{mediaInfo.videoCodec && (
						<div>
							<span style={{ color: "#8af" }}>Video:</span>{" "}
							{mediaInfo.videoCodec}
							{mediaInfo.width &&
								mediaInfo.height &&
								` (${mediaInfo.width}×${mediaInfo.height})`}
							{mediaInfo.fps && ` ${Math.round(mediaInfo.fps)} fps`}
							{mediaInfo.videoBitrate &&
								` ${Math.round(mediaInfo.videoBitrate / 1000)} kbps`}
						</div>
					)}

					<div>
						<span style={{ color: "#8af" }}>Audio:</span>{" "}
						{isAudioDisabled
							? "Disabled (AC-3 not supported)"
							: mediaInfo.audioCodec || "Detecting..."}
						{!isAudioDisabled &&
							mediaInfo.audioBitrate &&
							` ${Math.round(mediaInfo.audioBitrate / 1000)} kbps`}
					</div>
				</div>
			</div>
		</SMPopUp>
	);
};

PlaySMStreamDialog.displayName = "PlaySMStreamDialog";

export default React.memo(PlaySMStreamDialog);
