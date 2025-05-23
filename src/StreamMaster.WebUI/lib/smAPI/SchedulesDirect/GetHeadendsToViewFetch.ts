import { GetHeadendsToView } from '@lib/smAPI/SchedulesDirect/SchedulesDirectCommands';
import { Logger } from '@lib/common/logger';
import { createAsyncThunk } from '@reduxjs/toolkit';


export const fetchGetHeadendsToView = createAsyncThunk('cache/getGetHeadendsToView', async (_: void, thunkAPI) => {
  try {
    Logger.debug('Fetching GetHeadendsToView');
    const fetchDebug = localStorage.getItem('fetchDebug');
    const start = performance.now();
    const response = await GetHeadendsToView();
    if (fetchDebug) {
      const duration = performance.now() - start;
      Logger.debug(`Fetch GetHeadendsToView completed in ${duration.toFixed(2)}ms`);
    }

    return {param: _, value: response };
  } catch (error) {
    console.error('Failed to fetch', error);
    return thunkAPI.rejectWithValue({ error: error || 'Unknown error', value: undefined });
  }
});


