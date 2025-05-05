document.addEventListener("DOMContentLoaded", () => {
  // DOM elements
  const memoryMapContainer = document.getElementById("memory-map");
  const processJobBtn = document.getElementById("process-job-btn");
  const resetBtn = document.getElementById("reset-btn");
  const jobNameInput = document.getElementById("job-name");
  const jobSizeInput = document.getElementById("job-size");
  const pageSizeInput = document.getElementById("page-size");
  const messageDisplay = document.getElementById("message");
  const freeFramesInfo = document.getElementById("free-frames-info");
  const currentJobDetails = document.getElementById("current-job-details");
  const jobPmtContainer = document.getElementById("job-pmt-container");
  const outputPanel = document.getElementById("output-panel");
  const statusMessageContainer = document.getElementById("status-message-container");

  // Error popup elements
  const errorPopup = document.getElementById("error-popup");
  const errorMessage = document.getElementById("error-message");
  const closeErrorBtn = document.getElementById("close-error");

  // Progress bar element
  const progressBar = document.getElementById("allocation-progress-bar");

  // Memory simulation parameters
  const maxMemorySize = 60; // Total memory (in MB)
  const osSize = 10; // OS Kernel size (in MB)
  let frameSize = 5; // Size of each memory frame (in MB)
  let memory = Array(maxMemorySize).fill(null); // Initialize memory array with null values
  let jobs = []; // Array to store job records
  let jobCounter = 0; // Counter for auto-incrementing job names
  let jobColorMap = {}; // Map of job names to frame colors
  const jobColors = ["job1-frame", "job2-frame", "job3-frame"];
  let nextColorIndex = 0;
  let internalFragmentation = 0;

  // Set initial job name
  jobNameInput.value = `JOB-${jobCounter}`;

  // Initially hide UI elements
  statusMessageContainer.style.display = "none";
  outputPanel.style.display = "none";
  currentJobDetails.style.display = "none";
  jobPmtContainer.style.display = "none";

  // Initialize memory view with OS Kernel and empty frames
  const initializeMemoryView = () => {
    // Clear existing memory display
    memoryMapContainer.innerHTML = "";

    // Create OS Kernel segment (as a single box)
    const osSegment = document.createElement("div");
    osSegment.classList.add("memory-segment", "os-kernel");
    osSegment.innerHTML = `
      <div class="address">0 MB - ${osSize} MB</div>
      <div class="content">OS Kernel</div>
    `;
    memoryMapContainer.appendChild(osSegment);

    // Create the memory frames after OS kernel (10 frames, each 5MB)
    for (let i = osSize; i < maxMemorySize; i += frameSize) {
      const segment = document.createElement("div");
      segment.classList.add("memory-segment", "frame-free");
      const frameEnd = Math.min(i + frameSize, maxMemorySize);
      const frameNumber = Math.floor((i - osSize) / frameSize);

      segment.innerHTML = `
        <div class="address">${i} MB - ${frameEnd} MB</div>
        <div class="content">FRAME ${frameNumber}</div>
      `;
      memoryMapContainer.appendChild(segment);
    }

    // Initialize empty job details section
    currentJobDetails.innerHTML = `<h3 class="pmt-title">Jobs Information</h3>`;
    jobPmtContainer.innerHTML = `<h3 class="pmt-title">Page Map Tables</h3>`;

    // Update free frames info
    updateFreeFramesInfo();
  };

  // Update the memory visualization based on current state
  const updateMemoryView = () => {
    // Clear existing memory display and rebuild it
    memoryMapContainer.innerHTML = "";

    // Create OS Kernel segment (as a single box)
    const osSegment = document.createElement("div");
    osSegment.classList.add("memory-segment", "os-kernel");
    osSegment.innerHTML = `
      <div class="address">0 MB - ${osSize} MB</div>
      <div class="content">OS Kernel</div>
    `;
    memoryMapContainer.appendChild(osSegment);

    // Create the memory frames after OS kernel
    let totalFragmentation = 0;

    for (let i = osSize; i < maxMemorySize; i += frameSize) {
      const segment = document.createElement("div");
      segment.classList.add("memory-segment");
      const frameEnd = Math.min(i + frameSize, maxMemorySize);
      const frameNumber = Math.floor((i - osSize) / frameSize);

      // Check if this frame is occupied by a job
      let isOccupied = false;
      let occupyingJob = null;
      let occupyingPage = null;
      let fragmentedSpace = 0;

      // Check if any memory location in this frame is occupied
      for (let j = i; j < frameEnd; j++) {
        if (memory[j] !== null) {
          isOccupied = true;
          occupyingJob = memory[j].jobId;
          occupyingPage = memory[j].pageNumber;
          break;
        }
      }

      if (isOccupied) {
        // Count how many memory units in the frame are NULL (fragmented)
        for (let j = i; j < frameEnd; j++) {
          if (memory[j] === null) {
            fragmentedSpace++;
          }
        }

        // Add appropriate job-specific class and content
        segment.classList.add(jobColorMap[occupyingJob]);
        
        segment.innerHTML = `
          <div class="address">${i} MB - ${frameEnd} MB</div>
          <div class="content">JOB: ${occupyingJob} | PAGE: ${occupyingPage}</div>
        `;

        // Check if this frame has fragmentation and mark it
        if (fragmentedSpace > 0) {
          segment.classList.add("fragmented");
          
          // Create a special badge for fragmentation
          const fragmentBadge = document.createElement("span");
          fragmentBadge.classList.add("fragment-badge");
          fragmentBadge.textContent = `IF: ${fragmentedSpace}MB`;
          segment.appendChild(fragmentBadge);
          
          totalFragmentation += fragmentedSpace;
        }
      } else {
        // Empty frame
        segment.classList.add("frame-free");
        segment.innerHTML = `
          <div class="address">${i} MB - ${frameEnd} MB</div>
          <div class="content">FRAME ${frameNumber}</div>
        `;
      }

      memoryMapContainer.appendChild(segment);
    }

    // Update internal fragmentation
    internalFragmentation = totalFragmentation;
    updateFreeFramesInfo();
  };

  // Update the free frames information display
  const updateFreeFramesInfo = () => {
    const freeFrames = memory.slice(osSize).filter(item => item === null).length;
    const totalFrames = maxMemorySize - osSize;
    const usedFrames = totalFrames - freeFrames;
    
    freeFramesInfo.innerHTML = `
      <span>Free Memory: <strong>${freeFrames} MB</strong> of ${totalFrames} MB</span><br>
      <span>Used Memory: <strong>${usedFrames} MB</strong></span>
    `;
    
    // Add internal fragmentation info if present
    if (internalFragmentation > 0) {
      freeFramesInfo.innerHTML += `<br>
        <span>Internal Fragmentation: <strong>${internalFragmentation} MB</strong></span>
      `;
    }
  };

  // Display job details and PMT in the right panel
  const updateJobDetailsPanel = () => {
    if (jobs.length === 0) {
      currentJobDetails.style.display = "none";
      jobPmtContainer.style.display = "none";
      return;
    }

    // Show job details section
    currentJobDetails.style.display = "block";
    currentJobDetails.innerHTML = `<h3 class="pmt-title">Jobs Information</h3>`;

    // Create job detail boxes for each job
    jobs.forEach(job => {
      const jobBox = document.createElement("div");
      jobBox.classList.add("job-detail-box", `${jobColorMap[job.id]}-color`);
      
      jobBox.innerHTML = `
        <h3>${job.id}</h3>
        <p>Size: ${job.size} MB</p>
        <p>Pages: ${job.pages.length}</p>
      `;
      
      currentJobDetails.appendChild(jobBox);
    });

    // Show PMT section
    jobPmtContainer.style.display = "block";
    jobPmtContainer.innerHTML = `<h3 class="pmt-title">Page Map Tables</h3>`;

    // Create PMT table for each job
    jobs.forEach(job => {
      const jobColorClass = jobColorMap[job.id];
      const pmtTable = document.createElement("div");
      pmtTable.classList.add("pmt-table-container");
      
      let tableHTML = `
        <h4 class="${jobColorClass}-color">${job.id} - Page Map Table</h4>
        <table class="pmt-table">
          <thead>
            <tr>
              <th>Page No</th>
              <th>Size</th>
              <th>Frame No</th>
            </tr>
          </thead>
          <tbody>
      `;
      
      job.pages.forEach((page, index) => {
        // Calculate page size (last page might be smaller)
        const pageSize = (index === job.pages.length - 1 && job.size % frameSize !== 0) 
                        ? job.size % frameSize : frameSize;
        
        tableHTML += `
          <tr>
            <td>${index}</td>
            <td>${pageSize} MB</td>
            <td>${page.frameNumber}</td>
          </tr>
        `;
      });
      
      tableHTML += `
          </tbody>
        </table>
      `;
      
      pmtTable.innerHTML = tableHTML;
      jobPmtContainer.appendChild(pmtTable);
    });
  };

  // Allocate a specific frame for a job page with real-time display
  const allocateFrame = (frameNumber, job, pageIndex) => {
    return new Promise(resolve => {
      const frameElements = memoryMapContainer.querySelectorAll(".memory-segment");
      const frameElement = frameElements[frameNumber + 1]; // +1 for OS kernel
      
      if (!frameElement) {
        resolve();
        return;
      }
      
      // Update the frame content with job info immediately
      frameElement.classList.remove("frame-free");
      frameElement.classList.add(jobColorMap[job.id]);
      
      const contentDiv = frameElement.querySelector(".content");
      if (contentDiv) {
        contentDiv.textContent = `JOB: ${job.id} | PAGE: ${pageIndex}`;
      }
      
      // Add a longer delay for visual effect
      setTimeout(resolve, 200);
    });
  };

  // Process a new job with real-time allocation visualization
  const processJob = async () => {
    // Get job details from inputs
    const jobName = jobNameInput.value.trim() || `JOB-${jobCounter}`;
    const jobSize = parseInt(jobSizeInput.value);
    
    // Improved error handling
    if (isNaN(jobSize)) {
      showError("Please enter a valid number for job size.");
      return;
    }
    
    if (jobSize <= 0) {
      showError("Job size must be greater than 0 MB.");
      return;
    }
    
    if (jobSize > 50) {
      showError("Job size cannot exceed maximum available memory (50 MB).");
      return;
    }
    
    // Calculate number of pages needed
    const numPages = Math.ceil(jobSize / frameSize);
    
    // Count available frames
    const freeFrames = [];
    for (let i = 0; i < (maxMemorySize - osSize) / frameSize; i++) {
      let frameStart = osSize + (i * frameSize);
      let frameEnd = frameStart + frameSize;
      let isFrameFree = true;
      
      for (let j = frameStart; j < frameEnd; j++) {
        if (memory[j] !== null) {
          isFrameFree = false;
          break;
        }
      }
      
      if (isFrameFree) {
        freeFrames.push(i);
      }
    }
    
    // Enhanced error message with more details on available memory
    if (freeFrames.length < numPages) {
      const availableMemory = freeFrames.length * frameSize;
      showError(`Not enough memory available. Your job requires ${jobSize} MB (${numPages} frames), but only ${availableMemory} MB (${freeFrames.length} frames) are free.`);
      return;
    }
    
    // Assign a color to this job
    const jobColor = jobColors[nextColorIndex % jobColors.length];
    nextColorIndex++;
    jobColorMap[jobName] = jobColor;
    
    // Show UI panels
    outputPanel.style.display = "block";
    statusMessageContainer.style.display = "block";
    currentJobDetails.style.display = "block";
    jobPmtContainer.style.display = "block";
    
    // Initialize progress animation
    progressBar.style.width = "0%";
    messageDisplay.innerHTML = `Processing job ${jobName}...`;
    
    // Prepare job object
    const job = {
      id: jobName,
      size: jobSize,
      pages: []
    };
    
    // Randomly select frames to allocate (non-contiguous allocation)
    const selectedFrames = [];
    for (let i = 0; i < numPages; i++) {
      const randomIndex = Math.floor(Math.random() * freeFrames.length);
      const frameNumber = freeFrames[randomIndex];
      selectedFrames.push(frameNumber);
      freeFrames.splice(randomIndex, 1); // Remove allocated frame from free frames
    }
    
    // Allocate memory for each page with real-time visualization
    for (let i = 0; i < numPages; i++) {
      const frameNumber = selectedFrames[i];
      const frameStart = osSize + (frameNumber * frameSize);
      const frameEnd = Math.min(frameStart + frameSize, maxMemorySize);
      const pageSize = (i === numPages - 1 && jobSize % frameSize !== 0) ? 
                      jobSize % frameSize : frameSize;
      
      // Add page to job
      job.pages.push({
        frameNumber: frameNumber,
        pageNumber: i,
        size: pageSize
      });
      
      // Update memory array
      for (let j = frameStart; j < frameStart + pageSize; j++) {
        memory[j] = { jobId: jobName, pageNumber: i };
      }
      
      // Update progress bar
      progressBar.style.width = `${((i + 1) / numPages) * 100}%`;
      
      // Update memory visualization in real-time for this frame only
      await allocateFrame(frameNumber, job, i);
      
      // Increased delay between allocations (800ms instead of 300ms)
      await new Promise(resolve => setTimeout(resolve, 800));
    }
    
    // Add job to jobs array
    jobs.push(job);
    
    // Update the entire memory view for final state
    updateMemoryView();
    updateJobDetailsPanel();
    
    // Show success message
    messageDisplay.innerHTML = `Job ${jobName} successfully allocated.`;
    
    // Increment job counter for next job
    jobCounter++;
    jobNameInput.value = `JOB-${jobCounter}`;
    jobSizeInput.value = "";
  };

  // Reset memory and UI
  const resetMemory = () => {
    // Reset memory array
    memory = Array(maxMemorySize).fill(null);
    
    // Reset job tracking
    jobs = [];
    jobCounter = 0;
    jobNameInput.value = `JOB-${jobCounter}`;
    nextColorIndex = 0;
    internalFragmentation = 0;
    
    // Reset UI and hide panels
    initializeMemoryView();
    
    // Clear messages
    messageDisplay.innerHTML = "Memory reset successfully.";
    
    // Hide panels
    outputPanel.style.display = "none";
    currentJobDetails.style.display = "none";
    jobPmtContainer.style.display = "none";
    
    // Show only status message
    statusMessageContainer.style.display = "block";
  };

  // Show error popup
  const showError = (msg) => {
    errorMessage.textContent = msg;
    errorPopup.style.display = "block";
  };

  // Event Listeners
  processJobBtn.addEventListener("click", processJob);
  resetBtn.addEventListener("click", resetMemory);
  closeErrorBtn.addEventListener("click", () => {
    errorPopup.style.display = "none";
  });

  // Input validation for job size
  jobSizeInput.addEventListener("input", function() {
    const value = this.value;
    if (value && (isNaN(value) || parseInt(value) <= 0)) {
      this.classList.add("input-error");
    } else if (value && parseInt(value) > 50) {
      this.classList.add("input-error");
    } else {
      this.classList.remove("input-error");
    }
  });

  // Initialize the memory view on load
  initializeMemoryView();
});