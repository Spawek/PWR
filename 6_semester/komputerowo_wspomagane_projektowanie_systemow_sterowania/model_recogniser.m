function [] = model_recogniser(data_values, data_time)
%UNTITLED Summary of this function goes here
% Model output must return output for at least 100s

%% taking data to variables with shorter names
values = data_values;
time = data_time;

size = length(values);

%% calculating timeStep table (it's not constant in ode45)
timeStep = zeros(size, 1);
for i=2:size
    timeStep(i) = time(i) - time(i-1);
end


%% calculating derivative of output (which is actualy impulse answer)
derivative = zeros(size, 1);
for i=2:size
    derivative(i) = (values(i) - values(i-1)) / timeStep(i);
end

%% calculating 2nd derivative of output
derivative2 = zeros(size, 1);
for i=2:size
    derivative2(i) = (derivative(i) - derivative(i-1)) / timeStep(i);
end

%% recognising delay part
for i=2:size %i is important in next step
    if(values(i) ~= 0)
        delay = time(i);
        break;
    end
end

if(delay > 0.1)
    delayPartFound = true;
    
    %shifting values and time to non-zero part
    values = values(i:length(values));
    time = time(i:length(time));
    timeStep = timeStep(i:length(timeStep));
    derivative = derivative(i:length(derivative));
    derivative2 = derivative2(i:length(derivative2));
    size = size - i + 1;
    
else
    delayPartFound = false;
end
    
%% recognising integral part
Ki = (values(size) - values(round(size*0.75))) / (time(size) - time(round(size*0.75)));

if (Ki > 0.05)
    integralPartFound = true;
else
    integralPartFound = false;
end

%% recognising derivative part
Kd = values(1);

if (Kd > 0.10)
    derivativePartFound = true;
else
    derivativePartFound = false;
end

%% recognising oscilational part
% looking for time between TOP peeks
iterator = 1;
peaksTime = 0;
for i=2:size
   if(derivative(i-1) * derivative(i) < 0 && derivative(i) < 0)
      peaksTime(iterator) = time(i);
      iterator = iterator + 1;
   end
end

if(length(peaksTime) > 5)
    oscilationalPartExists = true;
    
    % calculating time between peaks
    timeBetweenPeaks = zeros(length(peaksTime)-1, 1);
    for i=2:length(peaksTime)
        %timeBetweenPeaks(1) would always be 0 -> so i-1 as index
        timeBetweenPeaks(i-1) = peaksTime(i) - peaksTime(i-1);
    end

    omega = 1/mean(timeBetweenPeaks) * (2 * pi);

    % acquiring peaks
    iterator = 1;
    for i=2:size
        % only top peeks <--- IMPORTANT!
        if(derivative(i-1) * derivative(i) < 0 && derivative(i) < 0) 
           peaks(iterator) = values(i);
           iterator = iterator + 1;
       end
    end

    %it's ~~~ value in middle of oscilation
    oscilationSteadyState = mean(values(round(length(values)*0.6):length(values))); %<---- it causes error

    %these are TOP peeks only 
    relativeChangeBetweenPeeks  = zeros(length(peaks)-1,1);
    for i=2:length(peaks)
        relativeChangeBetweenPeeks(i-1) = (peaks(i) - oscilationSteadyState) / (peaks(i-1) - oscilationSteadyState );
    end

    % http://pl.wikipedia.org/wiki/Cz³on_oscylacyjny
    sigma = -log(mean(relativeChangeBetweenPeeks)) / mean(timeBetweenPeaks);
else
    oscilationalPartExists = false;
end


%% recognising inertional part
if(integralPartFound)
    % ???
else
    inertionalPartFound = false;
    % ze strony p. Czemplik
    steadyState = values(size);
    for i=2:size
      if(abs(values(i) - steadyState * 0.632) > abs(values(i-1) - steadyState * 0.632))
         Tp = time(i);
         inertionalPartFound = true;
         break;
      end
    end
   
end

%% printing out answers
disp('Recognised parts in model:')

% oscilational part
if(oscilationalPartExists)
    disp('   Oscilational part:')
    disp(['      sigma = ', num2str(sigma)])
    disp(['      omega = ', num2str(omega)])
     disp('      transmittance:')
    disp(['        numerator:   ', num2str(oscilationSteadyState / (sigma * sigma + omega * omega))])
    disp(['        denominator:   s^2 +', num2str(2*sigma), 's + ', num2str(sigma * sigma + omega * omega)])
 end

% derivative part
if(derivativePartFound)
   disp('   Derivative part:')
   disp(['      Kd = ', num2str(Kd)])
   disp('      transmittance:')
   disp(['        numerator:   ', num2str(Kd), 's'])
   disp('        denominator:   1')
end

% integral part
if(integralPartFound)
   disp('   Integral part:')
   disp(['      Ki = ', num2str(Ki)])
   disp('      transmittance:')
   disp(['        numerator:   ', num2str(Ki)])
   disp(['        denominator:   s'])        
end

% inertional part 
if (inertionalPartFound)
   disp('   Inertional part:')
   disp(['      Tp = ', num2str(Tp)])
   disp('      transmittance:')
   disp(['        numerator:   ', num2str(values(length(values)))])
   disp(['        denominator:   ', num2str(Tp), 's + 1'])        
end

% delay part
if(delayPartFound)
   disp('   Delay part:')
   disp(['      Delay = ', num2str(delay)])
   disp('      transmittance:')
   disp(['        numerator:   e^(-', num2str(delay), 's)' ])
   disp(['        denominator:   1'])        
end

% 


end

